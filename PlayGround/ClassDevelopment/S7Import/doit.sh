#! /bin/bash
#start read.bat
cat db50.txt  >datenbausteinen.txt
cat db51.txt >>datenbausteinen.txt
cat db52.txt >>datenbausteinen.txt
cat db53.txt >>datenbausteinen.txt
cat db54.txt >>datenbausteinen.txt
cat db55.txt >>datenbausteinen.txt

grep   '\<BOOL\>'    datenbausteinen.txt >datenbausteinenbool.txt
sed -i '/\<BOOL\>/d' datenbausteinen.txt

ImportPLCSymbolik.sh     datenbausteinen.txt       >output.txt
ImportPLCSymbolikBool.sh datenbausteinenbool.txt  >>output.txt


awk '{print"INSERT INTO Plcitems(S7Adress) SELECT \""$1"\" WHERE NOT EXISTS(SELECT 1 FROM Plcitems WHERE S7Adress =\""$1"\");"}'  output.txt


awk 'BEGIN{FS="\t"}{print"UPDATE Plcitems SET S7Symbol=\""$2"\" WHERE S7Adress=\""$1"\";"}' output.txt

awk 'BEGIN{FS="\t"}{print"UPDATE Plcitems SET S7SymbolType=\""$3"\" WHERE S7Adress=\""$1"\";"}' output.txt

awk 'BEGIN{FS="\t"}{print"UPDATE Plcitems SET S7Comment=\""$4"\" WHERE S7Adress=\""$1"\";"}' output.txt

awk 'BEGIN{FS="\t"}{print"UPDATE Plcitems SET GroupComment=\""$5"\" WHERE S7Adress=\""$1"\";"}' output.txt

awk 'BEGIN{FS="\t"}{print"UPDATE Plcitems SET Comment=\""$6"\" WHERE S7Adress=\""$1"\";"}' output.txt

awk 'BEGIN{FS="\t"}{print"UPDATE Plcitems SET VisuSymbol=\""$7"\" WHERE S7Adress=\""$1"\";"}' output.txt
