DROP TABLE IF EXISTS Language;

CREATE TABLE Language 
(
ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
NativeText NVARCHAR(254),
De NVARCHAR(254),
En NVARCHAR(254),
Fr NVARCHAR(254),
Sp NVARCHAR(254),
Ru NVARCHAR(254)
);

INSERT INTO Language 
(NativeText, De, En, Fr, Sp, Ru)
VALUES
('NativeText'		, 'De'					, 'En'					,'Fr'	,'Sp'	,'Ru'),
('Aus[nl]Gebläse'	, 'Aus[nl]Gebläse'		, 'From[nl]Blower'		, ''	, ''	, '.юбьторпеавап'),
('Heizung'			, 'Heizung'				, 'Heat'				, ''	, ''	, 'фывфвф'),
('Luft'				, 'Luft'				, 'Air'					, ''	, ''	, ''),
('Wasser'           , 'Wasser'				, 'Water'				, ''	, ''	, 'фвфвфкуцерап'),
('Aus'              , 'Aus'					, 'Off'					, ''	, ''	, 'араракенуфвфыпав'),
('Menue'            , 'Menue'				, 'Menu'				, ''	, ''	, 'араракенуфвфыпав'),
('Sprache'          , 'Sprache'				, 'Language'			, ''	, ''	, 'араракенуфвфыпав'),
('Datenausgabe'          , 'Datenausgabe'				, 'Output data'			, ''	, ''	, 'араракенуфвфыпав'),
('Passworteingabe'  , 'Passworteingabe'		, 'Enter Password'		, ''	, ''	, 'выылнекуцуыфва'),
('Passwort:'		, 'Passwort:'			, 'Password:'			, ''	, ''	, 'цукенгшльотрип'),
('Tastatur'         , 'Tastatur'			, 'Keyboard'			, ''	, ''	, 'араракенуфвфыпав'),
('Programm beenden?', 'Programm beenden?'	, 'Exit Program?'		, ''	, ''	, 'длгонекпуц'),
('Programm beenden'	, 'Programm beenden'	, 'Exit Program'		, ''	, ''	, 'йцукенпгршщз'),
('Benutzer:','Benutzer:','User:','','',''),
('Operator','Operator','Operator','','',''),
('Ausgeloggt','Ausgeloggt','Logoff','','',''),
('Service','Service','Service','','',''),
('Pumpen 1','Pumpen 1','Pumps 1','','',''),
('Einrichter','Einrichter','Setter','','','');

CREATE TABLE IF NOT EXISTS Parameter  
(
ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
Parameter NVARCHAR(254),
Value NVARCHAR(254),
Comment NVARCHAR(254)
);


INSERT INTO Parameter(Parameter,Value,Comment)
SELECT 'Language','De','System'
WHERE NOT EXISTS(SELECT 1 FROM Parameter WHERE Parameter = 'Language');


DROP TABLE IF EXISTS Plcitems;

CREATE TABLE Plcitems 
(
ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
S7Adress NVARCHAR(20),
S7Symbol NVARCHAR(20),
S7SymbolType NVARCHAR(10),
S7Comment NVARCHAR(150),
SymbolType NVARCHAR(150),
RankingGroup NVARCHAR(150),
GroupComment NVARCHAR(150),
RankingSymbol NVARCHAR(150),
Comment NVARCHAR(150),
Unit NVARCHAR(150),
SymbolFormat NVARCHAR(150),
UpperLimit NVARCHAR(150),
LowerLimit NVARCHAR(150),
UserRightEnable NVARCHAR(150),
UserRightVisible NVARCHAR(150)
);

INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB50.DBD0','P1_Qmin_1','REAL','Pumpen 1:Pumpendruck 1','P','1','Pumpen 1','1','Pumpendruck 1','bar','0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB50.DBD4','P1_Qmin_2','REAL','Pumpen 1:Pumpendruck 2','P','1','Pumpen 1','2','Pumpendruck 2','bar','0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB50.DBD8','P1_Qmin_3','REAL','Pumpen 1:Pumpendruck 3','P','1','Pumpen 1','3','Pumpendruck 3','bar','0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB50.DBD12','P1_Qmin_4','REAL','Pumpen 1:Pumpendruck 4','P','1','Pumpen 1','4','Pumpendruck 4','bar','0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB50.DBD16','P1_Qmin_5','REAL','Pumpen 1:Pumpendruck 5','P','1','Pumpen 1','5','Pumpendruck 5','bar','0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB51.DBD0','P1_Qmin_1','REAL','Pumpen 2:Pumpendruck 1','P','2','Pumpen 2','1','Pumpendruck 1','bar','0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB51.DBD4','P1_Qmin_2','REAL','Pumpen 2:Pumpendruck 2','P','2','Pumpen 2','2','Pumpendruck 2','bar','0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB51.DBD8','P1_Qmin_3','REAL','Pumpen 2:Pumpendruck 3','P','2','Pumpen 2','3','Pumpendruck 3','bar','0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB51.DBD12','P1_Qmin_4','REAL','Pumpen 2:Pumpendruck 4','P','2','Pumpen 2','4','Pumpendruck 4','bar','0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB51.DBD16','P1_Qmin_5','REAL','Pumpen 2:Pumpendruck 5','P','2','Pumpen 2','5','Pumpendruck 5','bar','0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB52.DBW0','Valve01','INT','Schieber:Status 1','I','3','Schieber','1','Status 1',NULL,'0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB52.DBW2','Valve02','INT','Schieber:Status 2','I','3','Schieber','2','Status 2',NULL,'0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB52.DBW4','Valve03','INT','Schieber:Status 3','I','3','Schieber','3','Status 3',NULL,'0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB52.DBW6','Valve04','INT','Schieber:Status 4','I','3','Schieber','4','Status 4',NULL,'0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB52.DBW8','Valve05','INT','Schieber:Status 5','I','3','Schieber','5','Status 5',NULL,'0.00','99999','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX0.0','Bit01','BOOL','Fehlergruppe 1: Fehler 1','E','4','Fehlergruppe 1','1','Fehler 1',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX0.1','Bit01','BOOL','Fehlergruppe 1: Fehler 2','E','4','Fehlergruppe 1','2','Fehler 2',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX0.2','Bit02','BOOL','Fehlergruppe 1: Fehler 3','E','4','Fehlergruppe 1','3','Fehler 3',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX0.3','Bit03','BOOL','Fehlergruppe 1: Fehler 4','E','4','Fehlergruppe 1','4','Fehler 4',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX0.4','Bit04','BOOL','Fehlergruppe 1: Fehler 5','E','4','Fehlergruppe 1','5','Fehler 5',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX0.5','Bit05','BOOL','Fehlergruppe 1: Fehler 6','E','4','Fehlergruppe 1','6','Fehler 6',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX0.6','Bit06','BOOL','Fehlergruppe 1: Fehler 7','E','4','Fehlergruppe 1','7','Fehler 7',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX0.7','Bit07','BOOL','Fehlergruppe 1: Fehler 8','E','4','Fehlergruppe 1','8','Fehler 8',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX1.0','Bit08','BOOL','Fehlergruppe 2: Fehler 1','E','5','Fehlergruppe 2','1','Fehler 1',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX1.1','Bit09','BOOL','Fehlergruppe 2: Fehler 2','E','5','Fehlergruppe 2','2','Fehler 2',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX1.2','Bit10','BOOL','Fehlergruppe 2: Fehler 3','E','5','Fehlergruppe 2','3','Fehler 3',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX1.3','Bit11','BOOL','Fehlergruppe 2: Fehler 4','E','5','Fehlergruppe 2','4','Fehler 4',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX1.4','Bit12','BOOL','Fehlergruppe 2: Fehler 5','E','5','Fehlergruppe 2','5','Fehler 5',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX1.5','Bit13','BOOL','Fehlergruppe 2: Fehler 6','E','5','Fehlergruppe 2','6','Fehler 6',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX1.6','Bit14','BOOL','Fehlergruppe 2: Fehler 7','E','5','Fehlergruppe 2','7','Fehler 7',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX1.7','Bit15','BOOL','Fehlergruppe 2: Fehler 8','E','5','Fehlergruppe 2','8','Fehler 8',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX2.0','Bit16','BOOL','Fehlergruppe 3: Fehler 1','E','6','Fehlergruppe 3','1','Fehler 1',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX2.1','Bit17','BOOL','Fehlergruppe 3: Fehler 2','E','6','Fehlergruppe 3','2','Fehler 2',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX2.2','Bit18','BOOL','Fehlergruppe 3: Fehler 3','E','6','Fehlergruppe 3','3','Fehler 3',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX2.3','Bit29','BOOL','Fehlergruppe 3: Fehler 4','E','6','Fehlergruppe 3','4','Fehler 4',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX2.4','Bit20','BOOL','Fehlergruppe 3: Fehler 5','E','6','Fehlergruppe 3','5','Fehler 5',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX2.5','Bit21','BOOL','Fehlergruppe 3: Fehler 6','E','6','Fehlergruppe 3','6','Fehler 6',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX2.6','Bit22','BOOL','Fehlergruppe 3: Fehler 7','E','6','Fehlergruppe 3','7','Fehler 7',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX2.7','Bit23','BOOL','Fehlergruppe 3: Fehler 8','E','6','Fehlergruppe 3','8','Fehler 8',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX3.0','Bit24','BOOL','Fehlergruppe 4: Fehler 1','E','7','Fehlergruppe 4','1','Fehler 1',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX3.1','Bit25','BOOL','Fehlergruppe 4: Fehler 2','E','7','Fehlergruppe 4','2','Fehler 2',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX3.2','Bit26','BOOL','Fehlergruppe 4: Fehler 3','E','7','Fehlergruppe 4','3','Fehler 3',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX3.3','Bit27','BOOL','Fehlergruppe 4: Fehler 4','E','7','Fehlergruppe 4','4','Fehler 4',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX3.4','Bit28','BOOL','Fehlergruppe 4: Fehler 5','E','7','Fehlergruppe 4','5','Fehler 5',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX3.5','Bit29','BOOL','Fehlergruppe 4: Fehler 6','E','7','Fehlergruppe 4','6','Fehler 6',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX3.6','Bit30','BOOL','Fehlergruppe 4: Fehler 7','E','7','Fehlergruppe 4','7','Fehler 7',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB53.DBX3.7','Bit31','BOOL','Fehlergruppe 4: Fehler 8','E','7','Fehlergruppe 4','8','Fehler 8',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX0.0','Bit01','BOOL','Freigabegruppe 1: Freigabe 1','F','8','Freigabegruppe 1','1','Freigabe 1',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX0.1','Bit01','BOOL','Freigabegruppe 1: Freigabe 2','F','8','Freigabegruppe 1','2','Freigabe 2',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX0.2','Bit02','BOOL','Freigabegruppe 1: Freigabe 3','F','8','Freigabegruppe 1','3','Freigabe 3',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX0.3','Bit03','BOOL','Freigabegruppe 1: Freigabe 4','F','8','Freigabegruppe 1','4','Freigabe 4',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX0.4','Bit04','BOOL','Freigabegruppe 1: Freigabe 5','F','8','Freigabegruppe 1','5','Freigabe 5',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX0.5','Bit05','BOOL','Freigabegruppe 1: Freigabe 6','F','8','Freigabegruppe 1','6','Freigabe 6',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX0.6','Bit06','BOOL','Freigabegruppe 1: Freigabe 7','F','8','Freigabegruppe 1','7','Freigabe 7',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX0.7','Bit07','BOOL','Freigabegruppe 1: Freigabe 8','F','8','Freigabegruppe 1','8','Freigabe 8',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX1.0','Bit08','BOOL','Freigabegruppe 1: Freigabe 9','F','8','Freigabegruppe 1','9','Freigabe 9',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX1.1','Bit09','BOOL','Freigabegruppe 1: Freigabe 10','F','8','Freigabegruppe 1','10','Freigabe 10',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX1.2','Bit10','BOOL','Freigabegruppe 1: Freigabe 11','F','8','Freigabegruppe 1','11','Freigabe 11',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX1.3','Bit11','BOOL','Freigabegruppe 1: Freigabe 12','F','8','Freigabegruppe 1','12','Freigabe 12',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX1.4','Bit12','BOOL','Freigabegruppe 1: Freigabe 13','F','8','Freigabegruppe 1','13','Freigabe 13',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX1.5','Bit13','BOOL','Freigabegruppe 1: Freigabe 14','F','8','Freigabegruppe 1','14','Freigabe 14',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX1.6','Bit14','BOOL','Freigabegruppe 1: Freigabe 15','F','8','Freigabegruppe 1','15','Freigabe 15',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX1.7','Bit15','BOOL','Freigabegruppe 1: Freigabe 16','F','8','Freigabegruppe 1','16','Freigabe 16',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX2.0','Bit16','BOOL','Freigabegruppe 2: Freigabe 1','F','9','Freigabegruppe 2','1','Freigabe 1',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX2.1','Bit17','BOOL','Freigabegruppe 2: Freigabe 2','F','9','Freigabegruppe 2','2','Freigabe 2',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX2.2','Bit18','BOOL','Freigabegruppe 2: Freigabe 3','F','9','Freigabegruppe 2','3','Freigabe 3',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX2.3','Bit29','BOOL','Freigabegruppe 2: Freigabe 4','F','9','Freigabegruppe 2','4','Freigabe 4',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX2.4','Bit20','BOOL','Freigabegruppe 2: Freigabe 5','F','9','Freigabegruppe 2','5','Freigabe 5',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX2.5','Bit21','BOOL','Freigabegruppe 2: Freigabe 6','F','9','Freigabegruppe 2','6','Freigabe 6',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX2.6','Bit22','BOOL','Freigabegruppe 2: Freigabe 7','F','9','Freigabegruppe 2','7','Freigabe 7',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX2.7','Bit23','BOOL','Freigabegruppe 2: Freigabe 8','F','9','Freigabegruppe 2','8','Freigabe 8',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX3.0','Bit24','BOOL','Freigabegruppe 2: Freigabe 9','F','9','Freigabegruppe 2','9','Freigabe 9',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX3.1','Bit25','BOOL','Freigabegruppe 2: Freigabe 10','F','9','Freigabegruppe 2','10','Freigabe 10',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX3.2','Bit26','BOOL','Freigabegruppe 2: Freigabe 11','F','9','Freigabegruppe 2','11','Freigabe 11',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX3.3','Bit27','BOOL','Freigabegruppe 2: Freigabe 12','F','9','Freigabegruppe 2','12','Freigabe 12',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX3.4','Bit28','BOOL','Freigabegruppe 2: Freigabe 13','F','9','Freigabegruppe 2','13','Freigabe 13',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX3.5','Bit29','BOOL','Freigabegruppe 2: Freigabe 14','F','9','Freigabegruppe 2','14','Freigabe 14',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX3.6','Bit30','BOOL','Freigabegruppe 2: Freigabe 15','F','9','Freigabegruppe 2','15','Freigabe 15',NULL,'0','1','0','15','15');
INSERT INTO [Plcitems] ([S7Adress],[S7Symbol],[S7SymbolType],[S7Comment],[SymbolType],[RankingGroup],[GroupComment],[RankingSymbol],[Comment],[Unit],[SymbolFormat],[UpperLimit],[LowerLimit],[UserRightEnable],[UserRightVisible]) VALUES ('DB54.DBX3.7','Bit31','BOOL','Freigabegruppe 2: Freigabe 16','F','9','Freigabegruppe 2','16','Freigabe 16',NULL,'0','1','0','15','15');
