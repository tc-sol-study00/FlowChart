﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OilGsSimulationToObject {
    public class JidoShawithOil : JidoSha {
        public double SouSoukouKyori { get; set; }
        public double OilChangeInterval { get; set; }
        public double SouSoukouKyoriAtLastOilChange { get; set; }
        public double SouSoukouKyoriAtNextOilChange { get; set; }
        public JidoShawithOil(string argCarName,double argNenPi,double argTankFull,double argLimitPct,double argSouSoukouKyori) : base (argCarName, argNenPi, argTankFull, argLimitPct) {
            SouSoukouKyori = argSouSoukouKyori;                                 //総走行距離（新車納品時は0とする）
                                                                                //オイル交換関係変数
            OilChangeInterval = 5000.0;
            SouSoukouKyoriAtLastOilChange = SouSoukouKyori;                     //前回のオイル交換時の総走行距離
            SouSoukouKyoriAtNextOilChange = SouSoukouKyori + OilChangeInterval; //次回のオイル交換時の総走行距離
        }

        //走る（オーバライド）
        public double Soukou(int argSouKouKyori) {
            (double tankZanryo, double soukouKyori) = base.Soukou(argSouKouKyori);
            SouSoukouKyori += (double)soukouKyori;
            return soukouKyori;
        }
        //走る（家ーＧＳ間）
        public (double, double) RunBetweenHomeToGs(double argDist) {
            base.RunBetweenHomeToGs(argDist);
            SouSoukouKyori += argDist;
            return (TankZanryo, SouSoukouKyori);
        }
        //オイルチェック(サブクラス用）
        public bool OilCheck() {
            return (SouSoukouKyori < SouSoukouKyoriAtNextOilChange);
        }
    }
}

