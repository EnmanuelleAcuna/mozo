using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Transcription;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Transcripcion {
    public static class Transcriptor {
        public static async Task<string> Transcribir(byte[] Data) {
            // Crear firma de voz
            //CreateVoiceSignatureByUsingBody(@"VozFanny.wav").Wait();
            //CreateVoiceSignatureByUsingBody(@"VoiceEnmanuelle.wav").Wait();

            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            SpeechConfig Config = SpeechConfig.FromSubscription("1bd825a94b2f4b74bd50d080da63ca3f", "centralus");
            Config.SetProperty("ConversationTranscriptionInRoomAndOnline", "true");
            Config.SetServiceProperty("transcriptionMode", "async", ServicePropertyChannel.UriQueryParameter);

            // Create an audio stream from a wav file.
            Stream AudioStream = new MemoryStream(Data);
            BinaryReader Reader = new BinaryReader(AudioStream);
            AudioStreamFormat Format = Helper.ReadWaveHeader(Reader);
            AudioConfig AudioConfiguration = AudioConfig.FromStreamInput(new BinaryAudioStreamReader(Reader), Format);

            // Replace with your own audio file name.
            using (AudioConfig AudioInput = AudioConfiguration) {
                string Transcripcion = await TranscribirSimple(Config, AudioInput).ConfigureAwait(false);
                //string Transcripcion = await TranscribirConversacion(Config, AudioInput).ConfigureAwait(false);
                return Transcripcion;
            }
        }

        private static async Task<string> TranscribirSimple(SpeechConfig Config, AudioConfig AudioInput) {
            string Transcripcion = string.Empty;

            TaskCompletionSource<int> StopRecognition = new TaskCompletionSource<int>();

            // Creates a speech recognizer using audio stream input.
            using (SpeechRecognizer Recognizer = new SpeechRecognizer(Config, "es-MX", AudioInput)) {
                Recognizer.Recognized += (s, e) => {
                    if (e.Result.Reason == ResultReason.RecognizedSpeech) {
                        Transcripcion += e.Result.Text;
                    }
                    else if (e.Result.Reason == ResultReason.NoMatch) {
                        throw new Exception("NOMATCH: Speech could not be recognized.");
                    }
                };

                Recognizer.Canceled += (s, e) => {
                    if (e.Reason == CancellationReason.Error) {
                        throw new Exception($"CANCELED: ErrorCode={e.ErrorCode} ErrorDetails={e.ErrorDetails}");
                    }

                    StopRecognition.TrySetResult(0);
                };

                Recognizer.SessionStarted += (s, e) => {
                };

                Recognizer.SessionStopped += (s, e) => {
                    StopRecognition.TrySetResult(0);
                };

                // Starts continuous recognition.Uses StopContinuousRecognitionAsync() to stop recognition.
                await Recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                // Waits for completion.
                // Use Task.WaitAny to keep the task rooted.
                Task.WaitAny(new[] { StopRecognition.Task });

                // Stops recognition.
                await Recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);

                return Transcripcion;
            }
        }

        private static async Task<string> TranscribirConversacion(SpeechConfig Config, AudioConfig AudioInput) {
            string Transcripcion = string.Empty;

            TaskCompletionSource<int> StopRecognition = new TaskCompletionSource<int>();

            // Pick a conversation Id that is a GUID.
            String ConversationId = Guid.NewGuid().ToString();

            // Create a Conversation
            using (Conversation Conversation = await Conversation.CreateConversationAsync(Config, ConversationId).ConfigureAwait(false)) {
                // Create a conversation transcriber using audio stream input
                using (var Transcriber = new ConversationTranscriber(AudioInput)) {
                    await Transcriber.JoinConversationAsync(Conversation).ConfigureAwait(false);
                    Transcripcion += "Joined";
                    // Subscribe to events
                    Transcriber.Transcribed += (s, e) => {
                        if (e.Result.Reason == ResultReason.RecognizedSpeech) {
                            Transcripcion += e.Result.Text;
                        }
                        else if (e.Result.Reason == ResultReason.NoMatch) {
                            Transcripcion += $"NOMATCH: Speech could not be recognized.";
                            throw new Exception($"NOMATCH: Speech could not be recognized.");
                        }

                        Transcripcion += "Transcribed" + e.Result.Reason.ToString();
                    };

                    Transcriber.Canceled += (s, e) => {
                        if (e.Reason == CancellationReason.Error) {
                            throw new Exception($"CANCELED: ErrorCode={e.ErrorCode} ErrorDetails={e.ErrorDetails}");
                        }

                        Transcripcion += "Canceled";

                        StopRecognition.TrySetResult(0);
                    };

                    Transcriber.SessionStarted += (s, e) => {
                        Transcripcion += "Started";
                    };

                    Transcriber.SessionStopped += (s, e) => {
                        Transcripcion += "Stopped";
                        StopRecognition.TrySetResult(0);
                    };

                    await Transcriber.StartTranscribingAsync().ConfigureAwait(false);

                    // Waits for completion.
                    // Use Task.WaitAny to keep the task rooted.
                    Task.WaitAny(new[] { StopRecognition.Task });

                    await Transcriber.StopTranscribingAsync().ConfigureAwait(false);

                    return Transcripcion;
                }
            }
        }

        static async Task CreateVoiceSignatureByUsingBody(string FileName) {
            // Replace with your own region
            var Region = "centralus";
            // Change the name of the wave file to match yours
            byte[] FileBytes = File.ReadAllBytes(FileName);
            var Content = new ByteArrayContent(FileBytes);

            var Client = new HttpClient();
            // Add your subscription key to the header Ocp-Apim-Subscription-Key directly
            // Replace "YourSubscriptionKey" with your own subscription key
            Client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "1bd825a94b2f4b74bd50d080da63ca3f");
            // Edit with your desired region for `{region}`
            var Response = await Client.PostAsync($"https://signature.{Region}.cts.speech.microsoft.com/api/v1/Signature/GenerateVoiceSignatureFromByteArray", Content).ConfigureAwait(false);
            // A voice signature contains Version, Tag and Data key values from the Signature json structure from the Response body.
            // Voice signature format example: { "Version": <Numeric string or integer value>, "Tag": "string", "Data": "string" }
            var jsonData = await Response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
