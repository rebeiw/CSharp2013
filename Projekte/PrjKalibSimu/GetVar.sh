#!/bin/sh

grep = FrmRelease.cs |sort |uniq >Var_01.txt
 
sed -i '/=='/d Var_01.txt
sed -i '/!='/d Var_01.txt
 
 
 
sed -i 's/^ *//g' Var_01.txt

awk 'BEGIN {FS="="}{print$1}' Var_01.txt >Var_02.txt

sed -i 's/ *$//g' Var_02.txt

cat Var_02.txt |sort |uniq >Var_03.txt

grep -i " " Var_03.txt >Var_04.txt