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
('Programm beenden'	, 'Programm beenden'	, 'Exit Program'		, ''	, ''	, 'йцукенпгршщз');

CREATE TABLE IF NOT EXISTS Parameter  
(
ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
Parameter NVARCHAR(254),
Value NVARCHAR(254),
Comment NVARCHAR(254)
);

INSERT INTO PARAMETER(Parameter,Value,Comment)
SELECT 'Language','DE','SYSTEM'
WHERE NOT EXISTS (SELECT 1 FROM PARAMETER WHERE Parameter = 'Language');
