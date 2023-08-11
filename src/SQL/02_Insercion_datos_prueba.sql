GO
DELETE SGJD_ADM_TAB_USUARIOS
GO
DELETE SGJD_ADM_TAB_TIPOS_USUARIO
GO
DELETE SGJD_ADM_TAB_UNIDADES
GO
DELETE SGJD_ADM_TAB_ROLES_USUARIO
GO
DELETE SGJD_ADM_TAB_ROLES
GO
SET IDENTITY_INSERT [dbo].[SGJD_ADM_TAB_TIPOS_USUARIO] ON 
GO
INSERT [dbo].[SGJD_ADM_TAB_TIPOS_USUARIO] ([LLP_Id], [Nombre]) VALUES (1, N'Administrativo')
GO
INSERT [dbo].[SGJD_ADM_TAB_TIPOS_USUARIO] ([LLP_Id], [Nombre]) VALUES (2, N'Director')
GO
INSERT [dbo].[SGJD_ADM_TAB_TIPOS_USUARIO] ([LLP_Id], [Nombre]) VALUES (3, N'Expositor')
GO
INSERT [dbo].[SGJD_ADM_TAB_TIPOS_USUARIO] ([LLP_Id], [Nombre]) VALUES (5, N'Prueba')
GO
INSERT [dbo].[SGJD_ADM_TAB_TIPOS_USUARIO] ([LLP_Id], [Nombre]) VALUES (9, N'Prueba2')
GO
SET IDENTITY_INSERT [dbo].[SGJD_ADM_TAB_TIPOS_USUARIO] OFF
GO
SET IDENTITY_INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ON 
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (1, N'Gerencia', N'gerencia@gerencia.es')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (2, N'Presidencia Ejecutiva', N'presidencia@ejecutiva.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (3, N'Secretaría Técnica de Junta Directiva', N'secretaria@tecnica.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (4, N'Auditoría Interna', N'auditoria@interna.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (5, N'Asesoría Legal', N'asesoria@legal.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (6, N'Subgerencia Administrativa', N'subgerencia@administrativa.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (7, N'Subgerencia Técnica', N'subgerencia@tecnica.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (8, N'Unidad de Planificación y Evaluación', N'planificacion@evaluacion.co.cr')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (9, N'Unidad de Recursos Humanos', N'recursos@humanos.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (10, N'Unidad de Recursos Financieros', N'recursos@financieros.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (11, N'Asesoría de Cooperación Externa', N'asesoria@cooperacion.externa')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (12, N'Unidad de Compras Institucionales', N'compras@institucionales.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (13, N'Asesoría Control Interno', N'asesoria@control.interno')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (14, N'UPYME', N'upyme@ina.ac.cr')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (18, N'Unidad de prueba', N'prueba@ina.ac.cr')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (19, N'Unidad de prueba 2', N'unidad2@prueba.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (20, N'Unidad de prueba 3', N'unidad@prueba.com')
GO
INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] ([LLP_Id], [Nombre], [Correo]) VALUES (21, N'Junta Directiva', N'secretaria@ina.ac.cr')
GO
SET IDENTITY_INSERT [dbo].[SGJD_ADM_TAB_UNIDADES] OFF
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'0313b526-b67f-43d8-a3e8-8f4592fd8def', N'105710771', CAST(N'2020-07-09T18:26:23.857' AS DateTime), 1, N'calvaradomena@ina.ac.cr', N'APS7uMNJ6eO5ymBFWXB7L8nKUDRwPS7OqpIUVhxaqosFLIs7jy5mf3dThD2H/xJ00Q==', N'1dfa1959-ba42-437a-bad5-9c00563ee4b5', N'calvaradomena@ina.ac.cr', N'Carmen Alvarado Mena', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'05353e28-a456-447c-af43-ea7a66c5b2d7', N'202410380', CAST(N'2020-02-12T08:27:31.207' AS DateTime), 1, N'luzmil36@outlook.com', N'ADfdxZ5dCFtEffVjMxKmMhlWBz0URoU8nGlS9rmAokQvqy6NCK9Ff2589WZygqQmGg==', N'b66b3a20-142b-49d3-b467-733c843bbccb', N'claudio@gmail.com', N'Claudio Solano Cerdas', 2, 21)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'05d4a715-66d1-460c-928c-71d70e969bec', N'401480632', CAST(N'2020-07-10T23:53:28.847' AS DateTime), 1, N'jchaveszamora@ina.ac.cr', N'AK673/PpQW/PUj3j3yYbMCzAiWSBjOyrqqzgzfDV+Ljhog9AtXbIiJVNVm9MafEOSA==', N'884877ba-d1b9-4f5d-ae53-0de1c3da01f3', N'jchaveszamora@ina.ac.cr', N'Jose Roberto Chaves Zamora', 1, 1)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'08db63e9-ddf1-4f9b-a67f-8dc4fd0acfbb', N'106500311', CAST(N'2020-02-12T08:26:14.043' AS DateTime), 1, N'camogi@gmail.com', N'ADAEwu/c4z6a5Pjn2GNiky6TiSgpnx5VSsewQccTsUXtqejAQnfxDLDE14SYcmrWzA==', N'300b8a74-7fc7-4495-adc4-70e2b3bf3c00', N'carlos@gmaail.com', N'Carlos Humberto Montero Jiménez', 2, 21)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'0ed3cea6-cae6-4c03-ac71-cd839e17e1d7', N'104970682', CAST(N'2020-07-08T02:46:48.407' AS DateTime), 1, N'lfloresaguilar@ina.ac.cr', N'APq3drOjikdJVKn/QCj6n8beramQhZKTCTgZLPPg6pOKf9vjYIAGFRjl49JonCh21g==', N'23bcecc1-5d86-4646-a046-c046a3cac243', N'lfloresaguilar@ina.ac.cr', N'Leda Flores Aguilar', 1, 8)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'109aa559-44e5-4b49-8b99-d8464e36587d', N'123456789', CAST(N'2020-04-16T17:49:28.370' AS DateTime), 1, N'subauditor@prueba.com', N'AKawERnWNBSl0irdEI0jXLzP4CzdpoK6J/gWOQ86fSaMG5cm4AUdu+AzcSXbfbMqpw==', N'7c92d8ee-fef7-4ecf-bc90-a3616bde2af6', N'subauditor@prueba.com', N'Prueba Sub. Auditor', 5, 4)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'13c53904-c2dc-4a4f-ba31-71d300e1b390', N'108680269', CAST(N'2020-07-08T02:53:53.977' AS DateTime), 1, N'aaltamiranodiaz@ina.ac.cr', N'AM91eqvVgbbY1YfA52H8F/xs7guM0F6tON3RwnX0Gjh5Lzp/DD5tYotpR3uNPGXrjQ==', N'f73308df-31dc-4540-93d6-0008e7bc4f8f', N'aaltamiranodiaz@ina.ac.cr', N'Allan Altamirano Diaz', 1, 12)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'15f23af3-a150-4abe-8fb7-415bc0e22c37', N'401400016', CAST(N'2019-08-12T12:54:09.243' AS DateTime), 1, N'PHerreraVargas@ina.ac.cr', N'AITdHJQTHLv6EauQknsrbxj8HhvC1Jn+5xcOvw/S6+ds3w4lVMT29Fl0ZOyeXqDGFQ==', N'0007159e-82c1-4809-871b-15f67e11a5ee', N'patricia@ina.ac.cr', N'Patricia Herrera Vargas', 1, 3)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'225d0e44-8723-40f6-919f-3cb449a92c64', N'111510925', CAST(N'2020-02-12T08:22:18.877' AS DateTime), 1, N'geaninna@mtss.go.cr', N'ABqqCRY35Sd6mHXTB41N3HTSk+1dsrOrPaP04AkmrBYEWMDR+pKzzPoxwdGfGolTvg==', N'85cd6fda-3417-4477-92c7-d3bb7d5c0c2e', N'geaninna@mtss.go.cr', N'Geannina Dinarte Romero', 2, 21)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'294fb358-f733-4675-9e46-efd1a168dc05', N'112210052', CAST(N'2020-07-08T02:44:02.387' AS DateTime), 1, N'pmurillosalas@ina.ac.cr', N'AN6DtWYiBF9wGSxe9pLrIbcYcFbUDxNjVsNJKTyZ4qsG85AmY6xIzVKazHel1G3KwA==', N'07772986-a0de-4e3c-b4fe-271fed76959f', N'pmurillosalas@ina.ac.cr', N'Paula Murillo Salas', 1, 5)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'2b05bf54-1156-48f8-ba47-b562e0cba5f8', N'111490863', CAST(N'2019-03-22T09:56:30.890' AS DateTime), 1, N'sumana@datasoft.co.cr', N'ANxx4c3eHhbW8cFoizb5hP2PM5d/+DnBRv8fRkUenh6pUWlyl4G0mwp+oXgWCdQtEA==', N'8e0be9ce-71c4-485b-acd9-5b3b53cac758', N'sumana', N'Sharon Umaña Araya', 1, 1)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'2cd936e8-9d1c-43f9-9ba4-ed99033d011a', N'601660421', CAST(N'2019-08-01T09:02:49.357' AS DateTime), 1, N'mmoralesmontero@ina.ac.cr', N'APskBm43HaKT3m6RaNvFJ7icHfTBIdozVd/6jFpRdEffG8RepqNiLNk6NuGcwCxIsw==', N'ff84a9fe-1376-4cfc-a6d8-5d85ce797ae1', N'mmoralesmontero@ina.ac.cr', N'Maria Auxiliadora Morales Montero', 1, 3)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'2de0bdd0-4555-42e3-95f4-e6c117f2b44d', N'113030984', CAST(N'2020-05-20T14:06:54.947' AS DateTime), 1, N'pbermudezvillalobos@ina.ac.cr', N'AA8YBrKjmP2xeJRe/Oba1ACfhuJRftsc93hrzApq7xlgOZAKyhG1IBxQthUdSQ50dw==', N'8ce13cc9-e5f5-464b-8b35-6e362a5edc9d', N'pbermudezvillalobos@ina.ac.cr', N'Pamela Bermudez Villalobos', 1, 3)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'30837b49-1da6-4488-9542-8de4dd692c75', N'107110129', CAST(N'2020-07-08T02:50:30.533' AS DateTime), 1, N'xsolanochacon@ina.ac.cr', N'AJoSfc1Ray/6hjq9T47byzXSEKbGoopqMlSQBKflxiWTohiJBmTHeJihmcArr60uRA==', N'f24a93f1-8a2a-48a1-8ed3-b81e2d2e70cf', N'xsolanochacon@ina.ac.cr', N'Xinia Solano Chacon', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'324bbd62-924c-45fc-bcf1-39c183c06ef4', N'700880016', CAST(N'2020-07-08T18:27:34.170' AS DateTime), 1, N'jcastrobriceno@ina.ac.cr', N'AJmINFJCbxKhT0KvtQc8If2G2ae4TqpunD3MCVRVvM/KhQw0JnjsO6PlMuD8n7MeQg==', N'84f993f9-a42b-48d1-bed3-9fd52e9f57ef', N'jcastrobriceno@ina.ac.cr', N'Johanna Castro Briceño', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'35f8ae9d-680e-4490-99d1-fd931d35900e', N'204540622', CAST(N'2020-07-10T23:56:21.783' AS DateTime), 1, N'hrojasrojas@ina.ac.cr', N'ANoK3YDS2wbP4es7dC5It9NwskqRAIZP2VhSW1mZ8+8awM9ON2p2raOrVsGAUCdpbQ==', N'3274f08f-7f47-42e8-adc1-b3f8a022a841', N'hrojasrojas@ina.ac.cr', N'Helena Rojas Rojas', 1, 1)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'3868b26d-7031-4178-b8e1-8b1211e7182e', N'106070514', CAST(N'2020-02-12T08:37:38.210' AS DateTime), 1, N'lfmonge@gmail.com', N'AH+c/awd+iDhLHtgSf2MXYGXP5IGpNx3zJwUT63kQcBJuMKHroLtD4deaMJQKkHsHA==', N'35df19a1-1e93-4660-b789-c44b5d0ad587', N'luis@gmail.com', N'Luis Fernando Monge Rojas', 2, 21)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'38b712f9-cfd4-4e0d-81ef-df04cbf8e68b', N'501920782', CAST(N'2020-07-09T18:34:07.600' AS DateTime), 1, N'mmirandaramirez@ina.ac.cr', N'AFbE2NxeFw1W56esqISmyq8L6VIaz6i7urnqwU+SeY3V7GshbJmzeidxFabYbMzE7w==', N'dfd7cfbb-b56b-447d-ae2d-6dfb75204736', N'mmirandaramirez@ina.ac.cr', N'Milagro Miranda Ramirez', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'38ca9dc2-0b3d-46df-a07c-8143834a9bbc', N'107820209', CAST(N'2020-07-08T14:28:50.647' AS DateTime), 1, N'raguilarperez@ina.ac.cr', N'APhNgfdtTGoeqdvSMK7HqINzpCjpNfL2V2wLGLoweofiLi0O2gwZsS65ihY92NKEUA==', N'3de5dfb5-0ad8-4d58-892c-1797170ab017', N'raguilarperez@ina.ac.cr', N'Rocio Aguilar Perez', 1, 4)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'3f5866cd-f44a-4269-9770-3eb42af8a76a', N'108100921', CAST(N'2020-07-08T03:07:26.873' AS DateTime), 1, N'rzamoracruz@ina.ac.cr', N'ALWo0zrLHsjf1g3hHGNiFlKwcQwd6Sbx2Rzx8Jb930nLygROfzHNGojYL2JAY7fFJA==', N'85309b95-cb70-4122-ae6a-8fc0a2af079f', N'rzamoracruz@ina.ac.cr', N'Rodolfo Zamora Cruz', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'4372452a-197c-4c61-8594-67ae0bc0c634', N'116560362', CAST(N'2020-07-09T18:38:21.217' AS DateTime), 1, N'jvalverdeloaiza@ina.ac.cr', N'AHtGiVhQ3DnirQahjpNcP2SyKZCQX2QRD6QHagPFX509WlOb0BFq6vSQsHvNGPZbmg==', N'9e826412-f35b-4b5f-904f-9496975f9e2b', N'jvalverdeloaiza@ina.ac.cr', N'Jose Pablo Valverde Loiaza', 1, 2)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'43954926-090f-4523-aa65-1d09519f96db', N'401540207', CAST(N'2020-07-08T03:06:07.073' AS DateTime), 1, N'anunezchavarria@ina.ac.cr', N'AAb1HJlXnLx6kcJAxmrkE74qwHalIjNVihCHxpoT0oBhVST7ACPUlPwKF52Cgv2ydQ==', N'32edb6bc-226c-4560-8a84-aaf8b6909980', N'anunezchavarria@ina.ac.cr', N'Armando Nuñez Chavarria', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'4849a833-0a15-4313-b258-d030e24b4be9', N'602760126', CAST(N'2020-03-20T09:27:07.757' AS DateTime), 1, N'dchavarriachaves@ina.ac.cr', N'AApeTxLSP4fvukpZgxmssmaTq++hpp9sxmWuL4MzFawNJMlv7qewm6CikD8PIwhE+g==', N'c2b24dbe-bbdc-4191-81d4-599866d7cfbc', N'dchavarriachaves@ina.ac.cr', N'Didier Chavarría Chaves', 1, 4)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'4aaf96c9-e5eb-4fa9-934b-88b9abcc0416', N'105630358', CAST(N'2020-02-21T09:22:24.143' AS DateTime), 1, N'rmorabustamante@ina.ac.cr', N'AK/Ny4lEw5MCPNP5DLdZ8xEbibme0MgsDxR/s6YckZJBhab9cPwi3uLPO5bs40lELg==', N'9aee6657-3818-436c-a46f-57b83345ae7b', N'rmorabustamante@ina.ac.cr', N'Rita Mora Bustamante', 1, 4)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'4f9f287b-1f0f-47cf-a7c7-406c2b5a865a', N'206030724', CAST(N'2020-07-09T18:35:12.190' AS DateTime), 1, N'osalinasdiaz@ina.ac.cr', N'ANHGjwKSogAN6FNtl7MJoLlVJyVXrs2gDKuQO+Rckq8Cv4weX3lw5xFxubmq8PTb7g==', N'd2e0c4ed-b2cc-40a8-8944-b9a46c885eac', N'osalinasdiaz@ina.ac.cr', N'Obed Salinas Diaz', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'55687da8-debc-46af-a8b5-9b38145d7fd1', N'107860456', CAST(N'2020-05-20T14:08:27.813' AS DateTime), 1, N'asibajaesquivel@ina.ac.cr', N'ALrvjlfgs8U3T+sYja4J3DwiZ/Fz8vqqVWr4MsUNy4/VAEj7ZLM7xO5aLLcQAa4qCg==', N'78b7e245-7af9-4030-810f-58e553d7370d', N'asibajaesquivel@ina.ac.cr', N'Alba Sibaja Esquivel', 1, 3)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'6254bfb0-d7fb-492b-9d8e-4e73e135f875', N'115370797', CAST(N'2020-07-10T23:46:00.317' AS DateTime), 1, N'talvarezrojas@ina.ac.cr', N'ANczXEo4zi3s1dq8rcIWKumY7nkJo8A2uofI62ijkaxCVIcDLUILyjV6egBP6Adarg==', N'494fa0ae-dfe5-411b-a5b5-9edd4c9ca00c', N'talvarezrojas@ina.ac.cr', N'There Maria Alvarez Rojas', 1, 7)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'67872a26-7fcd-4f8a-a696-49fcda3dbac1', N'110760966', CAST(N'2020-07-08T03:03:38.133' AS DateTime), 1, N'cacunagarro@ina.ac.cr', N'AKvM2vxsqdG1xb7Fq7KAzIM+PpASctcPdT45BFZdEiscvZcGgr8xHJ3TfAKNTfmmTA==', N'4b6a841b-a7b9-444a-93ca-981a2f189e2f', N'cacunagarro@ina.ac.cr', N'Carlos Acuna Garro', 1, 10)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'6790c169-ee36-4556-9352-270b2658a327', N'106970524', CAST(N'2020-07-09T18:28:39.640' AS DateTime), 1, N'rjenkinsthompson@ina.ac.cr', N'AAhjKEwlfNSvzw0+ocbPTSen6kPOdwvxrx/ZqfF0sBg8HWa8o33h1g7g41YShroVEQ==', N'bf2c28ac-0d8f-4c16-b400-e7171a83b4cd', N'rjenkinsthompson@ina.ac.cr', N'Roxana Jenkins Hernandez', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'6f3b2c9f-a938-415b-b01c-ea9a33ba519b', N'114360003', CAST(N'2020-07-10T23:52:26.743' AS DateTime), 1, N'kvalverdealfaro@ina.ac.cr', N'AFK1L3iZJRpUXtptFymu/tevR0JHbSACKrOH94tJkCaFIzk4T81S56s/1uXFRK4lgA==', N'fb36182d-41d6-4fc4-8c59-db5471258223', N'kvalverdealfaro@ina.ac.cr', N'Karla Maria Valverde Alfaro', 1, 1)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'73eda171-6f7d-4bf5-bbbd-778770938fce', N'110700352', CAST(N'2020-07-08T03:00:54.957' AS DateTime), 1, N'mmaringuerrero@ina.ac.cr', N'ACWFUIQZIP51K2MhX9CAy1suxfRVn/LJT8Orfn2M/898kIEY4LB6GBnx0uFyxSMaOA==', N'933ea26f-c1b1-4ffb-950d-128d85d53803', N'mmaringuerrero@ina.ac.cr', N'Mariela Marin Guerrero', 1, 10)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'77f1b0e7-efa2-4512-8341-796606a2d889', N'105590751', CAST(N'2020-07-08T02:55:43.303' AS DateTime), 1, N'jvillalobosleiva@ina.ac.cr', N'AKQ035d7x+OzObUonUr2JiRoJ91hNk6lIvJIjVbqkYoCG/bXSNy59UIoFFp6lSlEjw==', N'5e130d6e-6aeb-4c1d-b360-d09e49c5666e', N'jvillalobosleiva@ina.ac.cr', N'Jose Manuel Villalobos Leiva', 1, 12)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'785a5b25-a8ba-4d74-9e45-50a48afeed14', N'116520463', CAST(N'2020-07-08T02:48:26.443' AS DateTime), 1, N'dfloresnunez@ina.ac.cr', N'AKwpSuED1jCzHcdyIyRwEDDWdjMOd/f3OGKPkUlv9ZdFH4bGoXaouGZr99riFqpxeg==', N'89af7acb-1ac1-43bb-91ba-f260dd7cb1bb', N'dfloresnunez@ina.ac.cr', N'Denisse Flores Nuñez', 1, 8)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'7c2f919b-0ea1-4987-9e1a-71dde3b675b0', N'1022204444', CAST(N'2020-01-17T10:58:01.893' AS DateTime), 1, N'admin@datasoft.co.cr', N'AFRU5rBWKJ/z5c5yi0NbOPn89ky3MlBrQgs4RDGEO4smI2HGQCWn4q2PQJcp8EWvag==', N'a0f441ca-525e-4862-835a-bef185852918', N'admin@datasoft.co.cr', N'Administrador', 5, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'81582e44-4e62-4a16-bf86-9103005613a4', N'604300799', CAST(N'2019-02-28T00:00:00.000' AS DateTime), 1, N'gsolis@datasoft.co.cr', N'AFVlBcYJE4IP99FmWYtQzR/1asT8j5BFFJriCNQPp6UdKq3ya5HEOZ4rnljnuMdCDg==', N'd17133ae-cf95-4748-b3b8-2e7c8783579d', N'gsolis', N'Sharel Carvajal Solis', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'8261d54a-472b-40de-8125-69414de11720', N'108650781', CAST(N'2020-07-08T02:37:28.637' AS DateTime), 1, N'lariasvalverde@ina.ac.cr', N'AGS9HKhbxXWcVVvOiPe+DsDfB0UfFqKW/Iz6FW8CoeQKPwYSh/jdD9WkQa8XJRMBZw==', N'532a6ed6-01ac-4acd-b085-bb8c129e61de', N'lariasvalverde@ina.ac.cr', N'Lidia Arias Valverde', 1, 4)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'87ee241e-4a06-48e3-a399-cb683e38102d', N'205500343', CAST(N'2020-07-08T02:39:17.300' AS DateTime), 1, N'jhernandezvargas@ina.ac.cr', N'ABjpZIRFvJ9RUu6885N6YOlLaKTnp424q9o2uyQpAvvYSlB0Rq/GWrmanza62dj/iQ==', N'4c0e25cc-bb27-4a96-961b-6ad1b608958f', N'jhernandezvargas@ina.ac.cr', N'Jose Alejandro Hernandez Vargas', 1, 5)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'8c27dcac-3b9e-4a34-a481-39f11d4814b7', N'303560902', CAST(N'2020-07-11T00:30:28.300' AS DateTime), 1, N'dvargasrodriguez@ina.ac.cr', N'APa9HLgX6EJvWlQa7CIUfQ0JujYnslW5DRxpUW7JyuBxfQu1n/lnegizOisYMhT2Rw==', N'a16c9d90-6e9e-46a2-9ad2-ba5c94a2f76c', N'dvargasrodriguez@ina.ac.cr', N'Deblin Vargas Rodriguez', 1, 1)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'8f737993-fc95-4af7-930a-28aa2bd8c21c', N'401380079', CAST(N'2020-07-09T18:27:32.693' AS DateTime), 1, N'cgonzalezchaverri@ina.ac.cr', N'AOrcLrjlxp0LLcLYI9xGiHVpIEVD9g33QB5AOM3jeTGYyqpfuiVymEWJqENjo+ZWLA==', N'3d6e69ee-c111-448c-aa71-110b9c8f463e', N'cgonzalezchaverri@ina.ac.cr', N'Carmen Gonzalez Chaverri', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'90eff4bb-6423-46fe-b0cf-0472a1a789e9', N'206830685', CAST(N'2019-03-22T10:38:58.613' AS DateTime), 1, N'eacuna@datasoft.co.cr', N'AIyICfCkpG+aTqva/jOpDm8LmLAlnAJe1VVzQEoxulS//DASwRdL0OVRkchUaFgMHQ==', N'0c2bbdc5-be5e-4436-9a02-585e660da46a', N'eacuna', N'Enmanuelle Acuña Arguedas', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'9415724d-d56a-4898-837f-e66d9cb7da80', N'603720805', CAST(N'2019-03-20T12:11:23.463' AS DateTime), 1, N'jrojastenorio@ina.ac.cr', N'AJcw55m7cp5MfaPngLVz4Jc50IokNDU3gtRSW6rEovjIOPLBGAkgbIue2o9gB4OEkw==', N'895bc90c-b312-48fa-a74d-ae8bc3a0bb58', N'jrojastenorio', N'Jonathan Rojas Tenorio', 1, 3)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'95a71e97-09c2-45b2-b6ce-23027730a5c5', N'207140412', CAST(N'2020-03-30T15:51:40.033' AS DateTime), 1, N'kmoya@datasoft.co.cr', N'ABPJCsKAloTyydYcO8w5ePXQrEi83E5BdkU2diQs49FoZT5kTmVf3Dg9dmuKIW13rA==', N'ece61896-a5cc-4968-a70f-600535a5c3bf', N'kmoya@datasoft.co.cr', N'Kevin Moya Rodríguez', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'99be7f4f-66cd-4737-98c2-2110bb94c11b', N'102220333', CAST(N'2020-02-12T08:53:39.463' AS DateTime), 0, N'ricardo@ina.ac.cr', N'AKUstjzvueMxAJhjrG1umJm4BSMoqsHYeESP+pJmoh1+ik5qDopAvLmOdtqNVMR4Fw==', N'f5d460c1-d87f-4805-bfe8-333db13217fe', N'ricardo@ina.ac.cr', N'Ricardo Arroyo Yannarella', 1, 5)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'9ae654bb-bca6-4b16-9017-c5d721d9d2e6', N'205750820', CAST(N'2020-07-08T19:18:42.637' AS DateTime), 1, N'vreyesaraya@ina.ac.cr', N'AGIlzWK+TMIFUsOKNh5BUx2s/avfQeLtUfc9J+W6iKLH5RMNSV2Mv6Tths4P+o4Tpw==', N'31b1b74c-7328-436e-8ad2-2051e38f8eb2', N'vreyesaraya@ina.ac.cr', N'Vielka Reyes Araya', 1, 10)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'9d30a790-1d9f-444e-90ec-b9d11a1af2e7', N'106020554', CAST(N'2019-09-18T08:29:53.573' AS DateTime), 1, N'jlizanosalazar@ina.ac.cr', N'AKuPb3PimmDdUoljY0WDpoJAipNyBi4S6YkIWZdFdYshjiqZa6tG8iD5Jo+/mjgkeA==', N'a514662a-b779-40b0-935e-a896cb3e51d1', N'jlizanosalazar', N'Jacqueline Lizano Salazar', 1, 3)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'9dfc4bd8-a7e0-4573-ac43-46fcb452139f', N'109900050', CAST(N'2020-02-12T08:23:46.953' AS DateTime), 1, N'ricardo.marin@mtss.go.cr', N'AKYNCmVZVGelw9v3holDxo/iM2fTfzZbDC0N2qroPECgmjfkAiV7RVTV/MpVwiw+Sw==', N'93708d44-fb10-4811-a582-50a54f6d5fe6', N'ricardo@mtss.go.cr', N'Ricardo Marín Azofeifa', 2, 21)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'9e30b562-fa81-4640-85e3-f198cf405e8a', N'113430819', CAST(N'2020-07-13T22:11:14.637' AS DateTime), 1, N'dhernandezsandoval@ina.ac.cr', N'AAEptoyHc1NTqov2oF4YkXKGZfCUVcrsvqksgm5frx2yxi5K5HRpFVoNZkKRjEJQng==', N'a210b130-ea54-4a08-a73a-e1fca34f55c8', N'dhernandezsandoval@ina.ac.cr', N'David Hernandez Sandoval', 1, 6)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'a0a8015d-c0e5-451e-b715-86c0314ba645', N'111690713', CAST(N'2019-08-01T09:04:56.393' AS DateTime), 1, N'avalencianoyamuni@ina.ac.cr', N'AG/USGOh6gbULSv1Kn4tD6w0Oy1ip5UupF5Wgrk/pXyQEkUogHXhMhDnvNS9hOz2Iw==', N'cf4b3912-f1f3-4c6a-866d-984cb4d52dcd', N'andres@ina.co.cr', N'Andrés Valenciano Yamuni', 2, 2)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'a1351bd3-ac93-4bb2-b530-d7fb10334612', N'204220647', CAST(N'2020-07-08T02:45:22.747' AS DateTime), 1, N'rramirezzuniga@ina.ac.cr', N'AASUC1Pu/I3YzcesTaVYegeIB6Tm9stncuDnk632j2spDnGD3oeLswE7FDFKk++xCA==', N'6af77dec-7ab7-43af-a1aa-42a614a89353', N'rramirezzuniga@ina.ac.cr', N'Roxana Ramirez Zuniga', 1, 5)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'a7f80363-e645-40eb-bb2d-27827b84912b', N'603690074', CAST(N'2020-07-08T02:57:21.903' AS DateTime), 1, N'jmedinabadilla@ina.ac.cr', N'ABiXIdOfFwn9GVbcFxN2hVUFSQ5y34m6PDMC2zWTl6FsRoaowznAnG+i4pQD0hPdLg==', N'6fe7a141-4027-4f1f-ac80-df8ac555abd9', N'jmedinabadilla@ina.ac.cr', N'Johan Felipe Medina Badilla', 1, 12)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'b4920626-7bc7-44ec-9e96-e8fa9775ab5f', N'113980617', CAST(N'2020-07-08T02:52:09.703' AS DateTime), 1, N'ngranadosperaza@ina.ac.cr', N'AJtCKYNuR5juunwzXPykfz4jTjzoc0pB9A1okWNad5xwos65tHQfkexh1t8XkMRZwg==', N'51eadfbd-e7ad-4e7a-88d8-731dcdd9dacd', N'ngranadosperaza@ina.ac.cr', N'Nancy Granados Peraza', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'b85da2fd-ec02-41fe-a20a-7c5db23b2366', N'206290665', CAST(N'2020-07-10T23:55:11.153' AS DateTime), 1, N'osolissalas@ina.ac.cr', N'AJGR7RtieQ+AB287NwPbSJoB0hQrfIgBAcBHN2TzI6/tctznqA+JT+H5z9sRaGkBXA==', N'2177fa76-eb51-4951-b69c-04d158638881', N'osolissalas@ina.ac.cr', N'Oscar Solis Salas', 1, 1)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'ba747669-b023-4d05-a924-702c904c0c2d', N'206300376', CAST(N'2020-07-11T00:29:00.327' AS DateTime), 1, N'srojassolano@ina.ac.cr', N'AJ2U28xtAUJqtETr9ShPIgoahdKRamsUinLVKvPOmYg1RIUbVvT9Bc8sCd3bpZChNg==', N'4b4341ca-5b01-4401-82a6-d9972160dfd7', N'srojassolano@ina.ac.cr', N'Sergio Daniel Rojas Solano', 1, 1)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'bc738dad-e199-4714-bbd7-32f8bc4e459b', N'106410120', CAST(N'2020-07-08T02:35:42.793' AS DateTime), 1, N'larayacisneros@ina.ac.cr', N'AIG9HuCXLB/ZAf+5vClVKoh0BNGugBRmhe3/95UCmpIW4n1D0B3SGIISo2PZUJBwzQ==', N'3658136a-ae8c-4233-80e4-96975c4d430f', N'larayacisneros@ina.ac.cr', N'Ligia Araya Cisneros', 1, 4)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'bdca3048-db3d-4a6e-802b-da5199508a8d', N'116080645', CAST(N'2020-05-19T17:13:43.027' AS DateTime), 1, N'caraya@datasoft.co.cr', N'AMYQYDijiGd5NdxwIwyqt0NVlRassHaGCNWPbBqJSp55Z/5P2/u40jKxWH2pmLO89w==', N'3bf66644-81ec-45a3-817b-2561325c274c', N'caraya@datasoft.co.cr', N'Christopher Araya', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'bf5c3849-b141-458d-b8ff-9ca48e42a9c7', N'114620904', CAST(N'2020-07-08T03:10:39.423' AS DateTime), 1, N'drojasportilla@ina.ac.cr', N'AOC1iw1FAJO4+8Wj3xv4Dp1eZVcYBDfC6eRgPz1x5Juz2qKcG/9bBOsEgBVD8uv69Q==', N'4ee1b690-0808-4abd-9d10-10b3437b9321', N'drojasportilla@ina.ac.cr', N'Daniela Rojas Portilla', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'c1689970-168c-46b8-9320-4c2869cbbb83', N'106890137', CAST(N'2020-07-09T18:29:54.440' AS DateTime), 1, N'jcastillosanchez@ina.ac.cr', N'AIUReRtHARTx558AwuWrXD43IonQRaYJNk5z2NJqsxD8VV9Kkb+mOOm/jB6DhPB30g==', N'd339ec23-8160-4923-83a3-99d811a87271', N'jcastillosanchez@ina.ac.cr', N'Jose Arturo Castillo Sanchez', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'c3bd8af3-62fd-4fe2-8699-733bf11ea884', N'116280736', CAST(N'2019-08-01T09:01:48.670' AS DateTime), 1, N'bbenavidesbenavides@ina.ac.cr', N'AFNCk/6l6z1YNwKzZtx5YEljsLSPDuFRx4pUi8H2SPpWPVwBtHF2Q7DXdbL14eIsQw==', N'2b88ab7e-8ce5-4647-a80e-a3f88896cf5f', N'bernardo@ina.co.cr', N'Bernardo Benavides Benavides', 1, 3)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'c6609d13-d771-4f5c-9b68-e59553ff7fde', N'104670076', CAST(N'2020-02-12T08:29:14.010' AS DateTime), 1, N'eleonorabadillasaxe@gmail.com', N'AFQYsBSIwLwZ53WqNRLCY+zacs8q0GM9/9579fN5z2lnvU0ShWx68Mdx+gIQqfcvKw==', N'47ff87f2-b0b7-4ca0-a28c-111aeb3a9ecf', N'eleonora@castrocarazo.ac.cr', N'Eleonora Badilla Saxe', 2, 21)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'c70835d3-f222-447e-aa39-ef91262c3381', N'401610979', CAST(N'2020-07-08T02:58:55.837' AS DateTime), 1, N'sesquivelalfaro@ina.ac.cr', N'ABxLBmsTj5gtgXrQs4jgfIvKLxw6wnBH6oemmnU9RvOByxae0xCaKvoONuJcyl9lqw==', N'2891f4f6-2985-4748-8e00-ea4164e5b504', N'sesquivelalfaro@ina.ac.cr', N'Silvia Esquivel Alfaro', 1, 10)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'c91ca28e-75ba-404b-bd56-e3c5cdcadf85', N'103640533', CAST(N'2020-07-08T02:43:06.110' AS DateTime), 1, N'jmorganrodriguez@ina.ac.cr', N'AJ3iH/9iUnJb9g+9JJQzy44U1or/dB6qUi8l/dKvJnK7yGOoaqsVDVIw9J+43M8u1Q==', N'829253eb-e513-45e0-9ca8-29675d59fbbe', N'jmorganrodriguez@ina.ac.cr', N'Jorge Morgan Rodriguez', 1, 5)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'd132dc50-1712-4fdc-a800-0447d686e3fa', N'700980574', CAST(N'2020-02-12T08:25:17.533' AS DateTime), 1, N'tayesnam70@hotmail.com', N'ALuUhtJMUQR3dysec/gxozG/sLzCglHFQKFMYMf8X8B9Q1OI+wSm88+BHTM3duGFCQ==', N'91693128-c247-4600-b659-6519527deafc', N'tyronne@hotmail.com', N'Tyronne Esna Montero', 2, 21)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'd2de5d50-d5ff-4193-a642-02f39e54affe', N'207460255', CAST(N'2020-03-23T21:16:56.410' AS DateTime), 1, N'jbarboza@datasoft.co.cr', N'AOmX06sx6wTvH21olgVt5e8xtoHxVU3bX2nMIIwNhV0aoy58Znrug3QPxE8J+MU1iw==', N'f7c5a965-a22a-4151-ac84-70946d29e4ab', N'jbarboza@datasoft.co.cr', N'Jackeline Barboza Sánchez', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'd90b360b-65cc-4dce-bcc3-1231ca79c240', N'304810238', CAST(N'2020-07-10T23:44:24.233' AS DateTime), 1, N'jmoranavarro@ina.ac.cr', N'AHPltMDc/8ihjAnekTisHvJCyc5h8xKfSPDpPhc7C9FlhDW1KxnNyqQMF71O0BSLrw==', N'1e81f820-6b90-40b0-9928-fa519565a971', N'jmoranavarro@ina.ac.cr', N'Jennifer Mora Navarro', 1, 2)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'd9b0b679-b8d1-44ff-88ab-7063850455d3', N'113960483', CAST(N'2020-07-10T23:48:29.250' AS DateTime), 1, N'lvegalopez@ina.ac.cr', N'AKWqs5mec8MB85QgY2HypMMZ3Ri/JPl543YIBjVLTpTCTyTiw4omNew4nKTsvPmiNg==', N'f125cf18-2e32-4471-aab1-ff2891ebf62f', N'lvegalopez@ina.ac.cr', N'Lizeth Vega Lopez', 1, 7)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'da349f29-126d-436f-b6e3-76724955eaeb', N'107940895', CAST(N'2020-07-11T00:27:48.217' AS DateTime), 1, N'jdurangonzalez@ina.ac.cr', N'ALmY/bRS8e8FGGw7UNTl93QthvbuxCM6BHRwfpMXxMCX5OQKGlD4ad2j3PVi5siVRA==', N'd5c257ff-7d49-4acc-8a74-e7941176b5f1', N'jdurangonzalez@ina.ac.cr', N'Julio Duran Gonzalez', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'e6be785b-3730-4936-91a8-2ae4e35a6cff', N'110420227', CAST(N'2020-07-13T22:10:20.180' AS DateTime), 1, N'aromerorodriguez@ina.ac.cr', N'AGz1N5vtkCaWpcOzpbIcHB8Q6oPNstG8Hp0oG0pU32p3147qOtCByigbZJFjfROKwA==', N'7f0483db-bd24-4039-a73d-c89ff474fce1', N'aromerorodriguez@ina.ac.cr', N'Andres Romero Rodriguez', 1, 7)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'e7227272-2d4a-496b-9ce6-f4d0e0b5dca7', N'107720759', CAST(N'2020-02-12T08:35:50.583' AS DateTime), 1, N'vgibson@cinde.org', N'AK2prkDwUNe7hsplHV/Qmg5fi20CgQNPVd07Ece511DzdQ6g9k26algALx2tCpVumw==', N'681d1eb7-6d89-4fe4-84d8-9d40e67e1194', N'vanessa@cinde.org', N'Vanessa Gibson Forbes', 2, 21)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'e8831cfe-e5b6-46b5-97b5-49415275d8d7', N'303780510', CAST(N'2020-02-12T08:19:22.803' AS DateTime), 1, N'melania.brenes.monge@mep.go.cr', N'AAmdNAowTWDvyvDJ4N0niCx+Yrd+7BCcgTakockZ0+r123Rug9cYLkKi3XGAO00wqQ==', N'e870a008-f911-4d4c-ad67-e5564ab20fff', N'melania@mep.go.cr', N'Melania Brenes Monge', 2, 21)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'ea2cdba7-4282-447b-8009-5e4cdbb119ed', N'402330297', CAST(N'2020-07-08T14:26:33.590' AS DateTime), 1, N'fsalazarsolis@ina.ac.cr', N'AJmPqOjEmAhphdy+dslwJPteQk960QlAHFz2hWBi2KLW1/AEBFEnlH7AWdqzTHUJ9A==', N'b2ab661f-8e0e-4655-88f7-b1426ebf147a', N'fsalazarsolis@ina.ac.cr', N'Fabiola Salazar Solis', 1, 5)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'eb239aaf-09ad-4d3f-bcfe-a1ed04d9be98', N'116280768', CAST(N'2019-03-20T11:33:20.083' AS DateTime), 1, N'dchavarria@datasoft.co.cr', N'AK1bBqfMNKs9F/0i6J1N+pNVel9RJSQAj/0uHCvX/tuKUS5MIFFW55hblAycpu1noQ==', N'd5add6b0-7ef8-478a-af05-29ed77ad0909', N'dchavarria', N'Derian Chavarría Zúñiga', 1, 19)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'ec09ed73-9c2e-4ab3-a2ef-a881559d24d6', N'123456788', CAST(N'2020-04-16T17:53:43.740' AS DateTime), 1, N'consulta@auditoria.com', N'AIOG6Jh9OsTOM3UUP9kcuPSMGgxc1fqylZvPj4Kz/niAROPirokbu2Y7He4culhTAw==', N'15ed60be-fecb-49cf-8a76-c6380766683c', N'consulta@auditoria.com', N'Usuario Consulta Auditoría', 5, 4)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'ec4ba1d7-aefb-4125-a6e5-489406238553', N'303950650', CAST(N'2020-07-09T18:32:17.070' AS DateTime), 1, N'erodriguezinces@ina.ac.cr', N'AOtrLctbHWx1x6q682xzrMoXlU+qt6v6+7ES7zsq+XSqMDpNid6Y3bMn99M8iCIulw==', N'8ea57cf0-f1a5-454d-80b4-5b482b7f5f06', N'erodriguezinces@ina.ac.cr', N'Erick Rodriguez Inces', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'eef08def-c41b-4d98-9a63-b0a511ab65f8', N'111020098', CAST(N'2020-07-08T03:09:37.357' AS DateTime), 1, N'acamachozamora@ina.ac.cr', N'AOpLGST58BymRNqOhhMuxmtkU520QK2tLcC8nbuOgcldnMgz0KQhyYr8qhRfaIK8uA==', N'c278cfc9-253d-4216-ade3-2dd632ed0fd9', N'acamachozamora@ina.ac.cr', N'Ana Laura Camacho Zamora', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'f2c40401-5226-421e-ac97-908b32547c3f', N'800790592', CAST(N'2019-05-17T10:54:57.533' AS DateTime), 1, N'fmrivas@ina.ac.cr', N'ADZrO8zgQXQrUg/bL69sO/gZNEguUHX6HovU0clEc5ZsPKl9VooqTJnmSolCAq/QwA==', N'd4345403-e18a-476a-a872-99dc64f306dc', N'fmrivas@ina.ac.cr', N'Flor Rivas Galdamez', 1, 18)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'f95090a3-dda7-4a75-a900-7b3943c5fa96', N'110480161', CAST(N'2020-02-12T08:52:47.480' AS DateTime), 1, N'sramirezgonzalez@ina.ac.cr', N'APKFPVFLJPZbsMdERB2TUqgpT98yUZU4lygt1WfNi0FcRCoCUt0OCzT9IwTsQKpwjQ==', N'c537b53f-ac18-4b28-a02a-9a204b3c6514', N'sofia@ina.ac.cr', N'Sofía Ramírez González', 1, 1)
GO
INSERT [dbo].[SGJD_ADM_TAB_USUARIOS] ([LLP_Id], [Cedula], [UltimaSesion], [Activo], [Email], [PasswordHash], [SecurityStamp], [UserName], [Nombre], [LLF_IdTipoUsuario], [LLF_IdUnidad]) VALUES (N'fbd0c954-6687-47a1-a785-cd42269eac67', N'113190699', CAST(N'2020-07-10T23:51:03.147' AS DateTime), 1, N'jsanchezcortes@ina.ac.cr', N'AAWNqQdUrIisjkQLD3ruGPBQ5BwSK2caj7xUR+orEXAdHsGK1SMGTPYKMkGHO7I1VA==', N'29c08a93-cb4b-47b3-88fc-a387e7f67fb4', N'jsanchezcortes@ina.ac.cr', N'Jorge Andres Sanchez Cortes ', 1, 1)
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES] ([Id], [Name]) VALUES (N'a3e4a00a-4299-42ed-9b24-e39fb9126194', N'Abogado Tecnico')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES] ([Id], [Name]) VALUES (N'1', N'Administrador')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES] ([Id], [Name]) VALUES (N'3ff3db1f-7d20-4130-a249-4fb277a1db3c', N'Auditor')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES] ([Id], [Name]) VALUES (N'2', N'Director')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES] ([Id], [Name]) VALUES (N'f657e69b-c0f3-4f81-b94a-499062e8f4d6', N'Presidencia')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES] ([Id], [Name]) VALUES (N'd2e005c5-0965-4069-a486-7957e636ded4', N'Profesional de apoyo')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES] ([Id], [Name]) VALUES (N'696832ca-bee4-4d00-b4a8-f215245b961c', N'Prueba')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES] ([Id], [Name]) VALUES (N'4586a2e8-4c7b-4c45-b03f-85d9d4dd03f8', N'Secretario Tecnico')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES] ([Id], [Name]) VALUES (N'10231a00-4ab8-4a61-8a9e-64ab66a8f370', N'Sub. Auditor')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES] ([Id], [Name]) VALUES (N'0ab46e1c-4ad8-4915-983e-ccd095f3f21e', N'Usuario De Consulta Auditoría')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'ec09ed73-9c2e-4ab3-a2ef-a881559d24d6', N'0ab46e1c-4ad8-4915-983e-ccd095f3f21e')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'0313b526-b67f-43d8-a3e8-8f4592fd8def', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'05353e28-a456-447c-af43-ea7a66c5b2d7', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'05d4a715-66d1-460c-928c-71d70e969bec', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'08db63e9-ddf1-4f9b-a67f-8dc4fd0acfbb', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'0ed3cea6-cae6-4c03-ac71-cd839e17e1d7', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'13c53904-c2dc-4a4f-ba31-71d300e1b390', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'15f23af3-a150-4abe-8fb7-415bc0e22c37', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'294fb358-f733-4675-9e46-efd1a168dc05', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'2b05bf54-1156-48f8-ba47-b562e0cba5f8', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'2cd936e8-9d1c-43f9-9ba4-ed99033d011a', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'2de0bdd0-4555-42e3-95f4-e6c117f2b44d', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'30837b49-1da6-4488-9542-8de4dd692c75', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'324bbd62-924c-45fc-bcf1-39c183c06ef4', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'35f8ae9d-680e-4490-99d1-fd931d35900e', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'3868b26d-7031-4178-b8e1-8b1211e7182e', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'38b712f9-cfd4-4e0d-81ef-df04cbf8e68b', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'38ca9dc2-0b3d-46df-a07c-8143834a9bbc', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'3f5866cd-f44a-4269-9770-3eb42af8a76a', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'4372452a-197c-4c61-8594-67ae0bc0c634', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'43954926-090f-4523-aa65-1d09519f96db', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'4849a833-0a15-4313-b258-d030e24b4be9', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'4aaf96c9-e5eb-4fa9-934b-88b9abcc0416', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'4f9f287b-1f0f-47cf-a7c7-406c2b5a865a', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'55687da8-debc-46af-a8b5-9b38145d7fd1', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'6254bfb0-d7fb-492b-9d8e-4e73e135f875', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'67872a26-7fcd-4f8a-a696-49fcda3dbac1', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'6790c169-ee36-4556-9352-270b2658a327', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'6f3b2c9f-a938-415b-b01c-ea9a33ba519b', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'73eda171-6f7d-4bf5-bbbd-778770938fce', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'77f1b0e7-efa2-4512-8341-796606a2d889', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'785a5b25-a8ba-4d74-9e45-50a48afeed14', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'7c2f919b-0ea1-4987-9e1a-71dde3b675b0', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'81582e44-4e62-4a16-bf86-9103005613a4', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'8261d54a-472b-40de-8125-69414de11720', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'87ee241e-4a06-48e3-a399-cb683e38102d', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'8c27dcac-3b9e-4a34-a481-39f11d4814b7', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'8f737993-fc95-4af7-930a-28aa2bd8c21c', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'90eff4bb-6423-46fe-b0cf-0472a1a789e9', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'9415724d-d56a-4898-837f-e66d9cb7da80', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'95a71e97-09c2-45b2-b6ce-23027730a5c5', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'9ae654bb-bca6-4b16-9017-c5d721d9d2e6', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'9d30a790-1d9f-444e-90ec-b9d11a1af2e7', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'9dfc4bd8-a7e0-4573-ac43-46fcb452139f', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'9e30b562-fa81-4640-85e3-f198cf405e8a', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'a1351bd3-ac93-4bb2-b530-d7fb10334612', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'a7f80363-e645-40eb-bb2d-27827b84912b', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'b4920626-7bc7-44ec-9e96-e8fa9775ab5f', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'b85da2fd-ec02-41fe-a20a-7c5db23b2366', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'ba747669-b023-4d05-a924-702c904c0c2d', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'bc738dad-e199-4714-bbd7-32f8bc4e459b', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'bdca3048-db3d-4a6e-802b-da5199508a8d', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'bf5c3849-b141-458d-b8ff-9ca48e42a9c7', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'c1689970-168c-46b8-9320-4c2869cbbb83', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'c6609d13-d771-4f5c-9b68-e59553ff7fde', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'c70835d3-f222-447e-aa39-ef91262c3381', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'c91ca28e-75ba-404b-bd56-e3c5cdcadf85', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'd132dc50-1712-4fdc-a800-0447d686e3fa', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'd2de5d50-d5ff-4193-a642-02f39e54affe', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'd90b360b-65cc-4dce-bcc3-1231ca79c240', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'd9b0b679-b8d1-44ff-88ab-7063850455d3', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'da349f29-126d-436f-b6e3-76724955eaeb', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'e6be785b-3730-4936-91a8-2ae4e35a6cff', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'e7227272-2d4a-496b-9ce6-f4d0e0b5dca7', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'e8831cfe-e5b6-46b5-97b5-49415275d8d7', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'ea2cdba7-4282-447b-8009-5e4cdbb119ed', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'eb239aaf-09ad-4d3f-bcfe-a1ed04d9be98', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'ec4ba1d7-aefb-4125-a6e5-489406238553', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'eef08def-c41b-4d98-9a63-b0a511ab65f8', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'f2c40401-5226-421e-ac97-908b32547c3f', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'f95090a3-dda7-4a75-a900-7b3943c5fa96', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'fbd0c954-6687-47a1-a785-cd42269eac67', N'1')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'109aa559-44e5-4b49-8b99-d8464e36587d', N'10231a00-4ab8-4a61-8a9e-64ab66a8f370')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'225d0e44-8723-40f6-919f-3cb449a92c64', N'2')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'a0a8015d-c0e5-451e-b715-86c0314ba645', N'2')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'c3bd8af3-62fd-4fe2-8699-733bf11ea884', N'4586a2e8-4c7b-4c45-b03f-85d9d4dd03f8')
GO
INSERT [dbo].[SGJD_ADM_TAB_ROLES_USUARIO] ([UserId], [RoleId]) VALUES (N'99be7f4f-66cd-4737-98c2-2110bb94c11b', N'a3e4a00a-4299-42ed-9b24-e39fb9126194')
GO
