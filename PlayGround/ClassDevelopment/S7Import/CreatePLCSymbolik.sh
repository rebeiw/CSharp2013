#! /bin/bash
cat db50.txt  >sps1.txt
cat db51.txt >>sps1.txt
cat db52.txt >>sps1.txt
cat db53.txt >>sps1.txt
cat db54.txt >>sps1.txt
cat db55.txt >>sps1.txt

awk 'BEGIN{FS="\t"}{print "            glb_plc.AddList(\""$3"\",\""$2"\",\""$4"\",\""$5"\");"}' sps1.txt >output.txt
