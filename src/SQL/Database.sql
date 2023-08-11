CREATE DATABASE Project_A 
ON PRIMARY (
	NAME = N'Project_A_Data', FILENAME = N'C:\Users\Emanu\Databases\Project_A_Data.mdf'
)
LOG ON (
	NAME = N'Project_A_Log', FILENAME = N'C:\Users\Emanu\Databases\Project_A_Log.ldf'
)