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
Adresse NVARCHAR(254),
Symbol NVARCHAR(254),
Symboltype NVARCHAR(254),
Kommentar NVARCHAR(254)
);

INSERT INTO Plcitems 
(Adresse, Symbol, Symboltype, Kommentar)
VALUES
('DB50.DBD0','P1_Qmin_1', 'REAL','Pumpen:Pumpendruck 1[EHbar]'),
('DB50.DBD4', 'P1_Qmin_2', 'REAL', 'Pumpen:Pumpendruck 2[EHbar]'),
('DB50.DBD8', 'P1_Qmin_3', 'REAL', 'Pumpen:Pumpendruck 3[EHbar]'),
('DB50.DBD12', 'P1_Qmin_4', 'REAL', 'Pumpen:Pumpendruck 4[EHbar]'),
('DB50.DBD16', 'P1_Qmin_5', 'REAL', 'Pumpen:Pumpendruck 5[EHbar]'),
('DB51.DBD0', 'P1_Qmin_1', 'REAL', 'Pumpen:Pumpendruck 1[EHbar]'),
('DB51.DBD4', 'P1_Qmin_2', 'REAL', 'Pumpen:Pumpendruck 2[EHbar]'),
('DB51.DBD8', 'P1_Qmin_3', 'REAL', 'Pumpen:Pumpendruck 3[EHbar]'),
('DB51.DBD12', 'P1_Qmin_4', 'REAL', 'Pumpen:Pumpendruck 4[EHbar]'),
('DB51.DBD16', 'P1_Qmin_5', 'REAL', 'Pumpen:Pumpendruck 5[EHbar]'),
('DB52.DBW0', 'Valve01', 'INT', 'Schieber:Status 1'),
('DB52.DBW2', 'Valve02', 'INT', 'Schieber:Status 2'),
('DB52.DBW4', 'Valve03', 'INT', 'Schieber:Status 3'),
('DB52.DBW6', 'Valve04', 'INT', 'Schieber:Status 4'),
('DB52.DBW8', 'Valve05', 'INT', 'Schieber:Status 5'),
('DB53.DBX0.0', 'Bit01', 'BOOL', ''),
('DB53.DBX0.1', 'Bit01', 'BOOL', ''),
('DB53.DBX0.2', 'Bit02', 'BOOL', ''),
('DB53.DBX0.3', 'Bit03', 'BOOL', ''),
('DB53.DBX0.4', 'Bit04', 'BOOL', ''),
('DB53.DBX0.5', 'Bit05', 'BOOL', ''),
('DB53.DBX0.6', 'Bit06', 'BOOL', ''),
('DB53.DBX0.7', 'Bit07', 'BOOL', ''),
('DB53.DBX1.0', 'Bit08', 'BOOL', ''),
('DB53.DBX1.1', 'Bit09', 'BOOL', ''),
('DB53.DBX1.2', 'Bit10', 'BOOL', ''),
('DB53.DBX1.3', 'Bit11', 'BOOL', ''),
('DB53.DBX1.4', 'Bit12', 'BOOL', ''),
('DB53.DBX1.5', 'Bit13', 'BOOL', ''),
('DB53.DBX1.6', 'Bit14', 'BOOL', ''),
('DB53.DBX1.7', 'Bit15', 'BOOL', ''),
('DB53.DBX2.0', 'Bit16', 'BOOL', ''),
('DB53.DBX2.1', 'Bit17', 'BOOL', ''),
('DB53.DBX2.2', 'Bit18', 'BOOL', ''),
('DB53.DBX2.3', 'Bit29', 'BOOL', ''),
('DB53.DBX2.4', 'Bit20', 'BOOL', ''),
('DB53.DBX2.5', 'Bit21', 'BOOL', ''),
('DB53.DBX2.6', 'Bit22', 'BOOL', ''),
('DB53.DBX2.7', 'Bit23', 'BOOL', ''),
('DB53.DBX3.0', 'Bit24', 'BOOL', ''),
('DB53.DBX3.1', 'Bit25', 'BOOL', ''),
('DB53.DBX3.2', 'Bit26', 'BOOL', ''),
('DB53.DBX3.3', 'Bit27', 'BOOL', ''),
('DB53.DBX3.4', 'Bit28', 'BOOL', ''),
('DB53.DBX3.5', 'Bit29', 'BOOL', ''),
('DB53.DBX3.6', 'Bit30', 'BOOL', ''),
('DB53.DBX3.7', 'Bit31', 'BOOL', ''),
('DB54.DBX0.0', 'Bit01', 'BOOL', ''),
('DB54.DBX0.1', 'Bit01', 'BOOL', ''),
('DB54.DBX0.2', 'Bit02', 'BOOL', ''),
('DB54.DBX0.3', 'Bit03', 'BOOL', ''),
('DB54.DBX0.4', 'Bit04', 'BOOL', ''),
('DB54.DBX0.5', 'Bit05', 'BOOL', ''),
('DB54.DBX0.6', 'Bit06', 'BOOL', ''),
('DB54.DBX0.7', 'Bit07', 'BOOL', ''),
('DB54.DBX1.0', 'Bit08', 'BOOL', ''),
('DB54.DBX1.1', 'Bit09', 'BOOL', ''),
('DB54.DBX1.2', 'Bit10', 'BOOL', ''),
('DB54.DBX1.3', 'Bit11', 'BOOL', ''),
('DB54.DBX1.4', 'Bit12', 'BOOL', ''),
('DB54.DBX1.5', 'Bit13', 'BOOL', ''),
('DB54.DBX1.6', 'Bit14', 'BOOL', ''),
('DB54.DBX1.7', 'Bit15', 'BOOL', ''),
('DB54.DBX2.0', 'Bit16', 'BOOL', ''),
('DB54.DBX2.1', 'Bit17', 'BOOL', ''),
('DB54.DBX2.2', 'Bit18', 'BOOL', ''),
('DB54.DBX2.3', 'Bit29', 'BOOL', ''),
('DB54.DBX2.4', 'Bit20', 'BOOL', ''),
('DB54.DBX2.5', 'Bit21', 'BOOL', ''),
('DB54.DBX2.6', 'Bit22', 'BOOL', ''),
('DB54.DBX2.7', 'Bit23', 'BOOL', ''),
('DB54.DBX3.0', 'Bit24', 'BOOL', ''),
('DB54.DBX3.1', 'Bit25', 'BOOL', ''),
('DB54.DBX3.2', 'Bit26', 'BOOL', ''),
('DB54.DBX3.3', 'Bit27', 'BOOL', ''),
('DB54.DBX3.4', 'Bit28', 'BOOL', ''),
('DB54.DBX3.5', 'Bit29', 'BOOL', ''),
('DB54.DBX3.6', 'Bit30', 'BOOL', ''),
('DB54.DBX3.7', 'Bit31', 'BOOL', '');
