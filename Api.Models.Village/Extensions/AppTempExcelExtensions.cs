namespace Api.Models;

public partial class AppTempExcel
{

    public void SetValue(int colIndex, string? value)
    {
        string temp = @"A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z
            ,AA,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL,AM,AN,AO,AP,AQ,AR,AS,AT,AU,AV,AW,AX,AY,AZ
            ,BA,BB,BC,BD,BE,BF,BG,BH,BI,BJ,BK,BL,BM,BN,BO,BP,BQ,BR,BS,BT,BU,BV,BW,BX,BY,BZ
            ,CA,CB,CC,CD,CE,CF,CG,CH,CI,CJ,CK,CL,CM,CN,CO,CP,CQ,CR,CS,CT,CU,CV,CW,CX,CY,CZ
            ,DA,DB,DC,DD,DE,DF,DG,DH,DI,DJ,DK,DL,DM,DN,DO,DP,DQ,DR,DS,DT,DU,DV,DW,DX,DY,DZ
            ,EA,EB,EC,ED,EE,EF,EG,EH,EI,EJ,EK,EL,EM,EN,EO,EP,EQ,ER,ES,ET,EU,EV,EW,EX,EY,EZ
            ,FA,FB,FC,FD,FE,FF,FG,FH,FI,FJ,FK,FL,FM,FN,FO,FP,FQ,FR,FS,FT,FU,FV,FW,FX,FY,FZ
            ,GA,GB,GC,GD,GE,GF,GG,GH,GI,GJ,GK,GL,GM,GN,GO,GP,GQ,GR,GS,GT,GU,GV,GW,GX,GY,GZ
            ,HA,HB,HC,HD,HE,HF,HG,HH,HI,HJ,HK,HL,HM,HN,HO,HP,HQ,HR,HS,HT,HU,HV,HW,HX,HY,HZ
            ,N1,N2,N3,N4,N5,N6,N7,N8,N9,N10
            ,D1,D2,D3,D4,D5,D6,D7,D8,D9,D10";

        string[] colArray = temp.Split(",");
        this.SetValue(colArray[colIndex], value);
    }

    public void SetValue(string col, string? value)
    {
        switch (col)
        {
            case "A":
                this.A = value;
                break;
            case "B":
                this.B = value;
                break;
            case "C":
                this.C = value;
                break;
            case "D":
                this.D = value;
                break;
            case "E":
                this.E = value;
                break;
            case "F":
                this.F = value;
                break;
            case "G":
                this.G = value;
                break;
            case "H":
                this.H = value;
                break;
            case "I":
                this.I = value;
                break;
            case "J":
                this.J = value;
                break;
            case "K":
                this.K = value;
                break;
            case "L":
                this.L = value;
                break;
            case "M":
                this.M = value;
                break;
            case "N":
                this.N = value;
                break;
            case "O":
                this.O = value;
                break;
            case "P":
                this.P = value;
                break;
            case "Q":
                this.Q = value;
                break;
            case "R":
                this.R = value;
                break;
            case "S":
                this.S = value;
                break;
            case "T":
                this.T = value;
                break;
            case "U":
                this.U = value;
                break;
            case "V":
                this.V = value;
                break;
            case "W":
                this.W = value;
                break;
            case "X":
                this.X = value;
                break;
            case "Y":
                this.Y = value;
                break;
            case "Z":
                this.Z = value;
                break;
            case "AA":
                this.AA = value;
                break;
            case "AB":
                this.AB = value;
                break;
            case "AC":
                this.AC = value;
                break;
            case "AD":
                this.AD = value;
                break;
            case "AE":
                this.AE = value;
                break;
            case "AF":
                this.AF = value;
                break;
            case "AG":
                this.AG = value;
                break;
            case "AH":
                this.AH = value;
                break;
            case "AI":
                this.AI = value;
                break;
            case "AJ":
                this.AJ = value;
                break;
            case "AK":
                this.AK = value;
                break;
            case "AL":
                this.AL = value;
                break;
            case "AM":
                this.AM = value;
                break;
            case "AN":
                this.AN = value;
                break;
            case "AO":
                this.AO = value;
                break;
            case "AP":
                this.AP = value;
                break;
            case "AQ":
                this.AQ = value;
                break;
            case "AR":
                this.AR = value;
                break;
            case "AS":
                this.AS = value;
                break;
            case "AT":
                this.AT = value;
                break;
            case "AU":
                this.AU = value;
                break;
            case "AV":
                this.AV = value;
                break;
            case "AW":
                this.AW = value;
                break;
            case "AX":
                this.AX = value;
                break;
            case "AY":
                this.AY = value;
                break;
            case "AZ":
                this.AZ = value;
                break;
            case "BA":
                this.BA = value;
                break;
            case "BB":
                this.BB = value;
                break;
            case "BC":
                this.BC = value;
                break;
            case "BD":
                this.BD = value;
                break;
            case "BE":
                this.BE = value;
                break;
            case "BF":
                this.BF = value;
                break;
            case "BG":
                this.BG = value;
                break;
            case "BH":
                this.BH = value;
                break;
            case "BI":
                this.BI = value;
                break;
            case "BJ":
                this.BJ = value;
                break;
            case "BK":
                this.BK = value;
                break;
            case "BL":
                this.BL = value;
                break;
            case "BM":
                this.BM = value;
                break;
            case "BN":
                this.BN = value;
                break;
            case "BO":
                this.BO = value;
                break;
            case "BP":
                this.BP = value;
                break;
            case "BQ":
                this.BQ = value;
                break;
            case "BR":
                this.BR = value;
                break;
            case "BS":
                this.BS = value;
                break;
            case "BT":
                this.BT = value;
                break;
            case "BU":
                this.BU = value;
                break;
            case "BV":
                this.BV = value;
                break;
            case "BW":
                this.BW = value;
                break;
            case "BX":
                this.BX = value;
                break;
            case "BY":
                this.BY = value;
                break;
            case "BZ":
                this.BZ = value;
                break;
            case "CA":
                this.CA = value;
                break;
            case "CB":
                this.CB = value;
                break;
            case "CC":
                this.CC = value;
                break;
            case "CD":
                this.CD = value;
                break;
            case "CE":
                this.CE = value;
                break;
            case "CF":
                this.CF = value;
                break;
            case "CG":
                this.CG = value;
                break;
            case "CH":
                this.CH = value;
                break;
            case "CI":
                this.CI = value;
                break;
            case "CJ":
                this.CJ = value;
                break;
            case "CK":
                this.CK = value;
                break;
            case "CL":
                this.CL = value;
                break;
            case "CM":
                this.CM = value;
                break;
            case "CN":
                this.CN = value;
                break;
            case "CO":
                this.CO = value;
                break;
            case "CP":
                this.CP = value;
                break;
            case "CQ":
                this.CQ = value;
                break;
            case "CR":
                this.CR = value;
                break;
            case "CS":
                this.CS = value;
                break;
            case "CT":
                this.CT = value;
                break;
            case "CU":
                this.CU = value;
                break;
            case "CV":
                this.CV = value;
                break;
            case "CW":
                this.CW = value;
                break;
            case "CX":
                this.CX = value;
                break;
            case "CY":
                this.CY = value;
                break;
            case "CZ":
                this.CZ = value;
                break;
            case "DA":
                this.DA = value;
                break;
            case "DB":
                this.DB = value;
                break;
            case "DC":
                this.DC = value;
                break;
            case "DD":
                this.DD = value;
                break;
            case "DE":
                this.DE = value;
                break;
            case "DF":
                this.DF = value;
                break;
            case "DG":
                this.DG = value;
                break;
            case "DH":
                this.DH = value;
                break;
            case "DI":
                this.DI = value;
                break;
            case "DJ":
                this.DJ = value;
                break;
            case "DK":
                this.DK = value;
                break;
            case "DL":
                this.DL = value;
                break;
            case "DM":
                this.DM = value;
                break;
            case "DN":
                this.DN = value;
                break;
            case "DO":
                this.DO = value;
                break;
            case "DP":
                this.DP = value;
                break;
            case "DQ":
                this.DQ = value;
                break;
            case "DR":
                this.DR = value;
                break;
            case "DS":
                this.DS = value;
                break;
            case "DT":
                this.DT = value;
                break;
            case "DU":
                this.DU = value;
                break;
            case "DV":
                this.DV = value;
                break;
            case "DW":
                this.DW = value;
                break;
            case "DX":
                this.DX = value;
                break;
            case "DY":
                this.DY = value;
                break;
            case "DZ":
                this.DZ = value;
                break;
            case "EA":
                this.EA = value;
                break;
            case "EB":
                this.EB = value;
                break;
            case "EC":
                this.EC = value;
                break;
            case "ED":
                this.ED = value;
                break;
            case "EE":
                this.EE = value;
                break;
            case "EF":
                this.EF = value;
                break;
            case "EG":
                this.EG = value;
                break;
            case "EH":
                this.EH = value;
                break;
            case "EI":
                this.EI = value;
                break;
            case "EJ":
                this.EJ = value;
                break;
            case "EK":
                this.EK = value;
                break;
            case "EL":
                this.EL = value;
                break;
            case "EM":
                this.EM = value;
                break;
            case "EN":
                this.EN = value;
                break;
            case "EO":
                this.EO = value;
                break;
            case "EP":
                this.EP = value;
                break;
            case "EQ":
                this.EQ = value;
                break;
            case "ER":
                this.ER = value;
                break;
            case "ES":
                this.ES = value;
                break;
            case "ET":
                this.ET = value;
                break;
            case "EU":
                this.EU = value;
                break;
            case "EV":
                this.EV = value;
                break;
            case "EW":
                this.EW = value;
                break;
            case "EX":
                this.EX = value;
                break;
            case "EY":
                this.EY = value;
                break;
            case "EZ":
                this.EZ = value;
                break;
            case "FA":
                this.FA = value;
                break;
            case "FB":
                this.FB = value;
                break;
            case "FC":
                this.FC = value;
                break;
            case "FD":
                this.FD = value;
                break;
            case "FE":
                this.FE = value;
                break;
            case "FF":
                this.FF = value;
                break;
            case "FG":
                this.FG = value;
                break;
            case "FH":
                this.FH = value;
                break;
            case "FI":
                this.FI = value;
                break;
            case "FJ":
                this.FJ = value;
                break;
            case "FK":
                this.FK = value;
                break;
            case "FL":
                this.FL = value;
                break;
            case "FM":
                this.FM = value;
                break;
            case "FN":
                this.FN = value;
                break;
            case "FO":
                this.FO = value;
                break;
            case "FP":
                this.FP = value;
                break;
            case "FQ":
                this.FQ = value;
                break;
            case "FR":
                this.FR = value;
                break;
            case "FS":
                this.FS = value;
                break;
            case "FT":
                this.FT = value;
                break;
            case "FU":
                this.FU = value;
                break;
            case "FV":
                this.FV = value;
                break;
            case "FW":
                this.FW = value;
                break;
            case "FX":
                this.FX = value;
                break;
            case "FY":
                this.FY = value;
                break;
            case "FZ":
                this.FZ = value;
                break;
            case "GA":
                this.GA = value;
                break;
            case "GB":
                this.GB = value;
                break;
            case "GC":
                this.GC = value;
                break;
            case "GD":
                this.GD = value;
                break;
            case "GE":
                this.GE = value;
                break;
            case "GF":
                this.GF = value;
                break;
            case "GG":
                this.GG = value;
                break;
            case "GH":
                this.GH = value;
                break;
            case "GI":
                this.GI = value;
                break;
            case "GJ":
                this.GJ = value;
                break;
            case "GK":
                this.GK = value;
                break;
            case "GL":
                this.GL = value;
                break;
            case "GM":
                this.GM = value;
                break;
            case "GN":
                this.GN = value;
                break;
            case "GO":
                this.GO = value;
                break;
            case "GP":
                this.GP = value;
                break;
            case "GQ":
                this.GQ = value;
                break;
            case "GR":
                this.GR = value;
                break;
            case "GS":
                this.GS = value;
                break;
            case "GT":
                this.GT = value;
                break;
            case "GU":
                this.GU = value;
                break;
            case "GV":
                this.GV = value;
                break;
            case "GW":
                this.GW = value;
                break;
            case "GX":
                this.GX = value;
                break;
            case "GY":
                this.GY = value;
                break;
            case "GZ":
                this.GZ = value;
                break;
            case "HA":
                this.HA = value;
                break;
            case "HB":
                this.HB = value;
                break;
            case "HC":
                this.HC = value;
                break;
            case "HD":
                this.HD = value;
                break;
            case "HE":
                this.HE = value;
                break;
            case "HF":
                this.HF = value;
                break;
            case "HG":
                this.HG = value;
                break;
            case "HH":
                this.HH = value;
                break;
            case "HI":
                this.HI = value;
                break;
            case "HJ":
                this.HJ = value;
                break;
            case "HK":
                this.HK = value;
                break;
            case "HL":
                this.HL = value;
                break;
            case "HM":
                this.HM = value;
                break;
            case "HN":
                this.HN = value;
                break;
            case "HO":
                this.HO = value;
                break;
            case "HP":
                this.HP = value;
                break;
            case "HQ":
                this.HQ = value;
                break;
            case "HR":
                this.HR = value;
                break;
            case "HS":
                this.HS = value;
                break;
            case "HT":
                this.HT = value;
                break;
            case "HU":
                this.HU = value;
                break;
            case "HV":
                this.HV = value;
                break;
            case "HW":
                this.HW = value;
                break;
            case "HX":
                this.HX = value;
                break;
            case "HY":
                this.HY = value;
                break;
            case "HZ":
                this.HZ = value;
                break;
            case "N1":
                this.N1 = decimal.TryParse(value, out var n1Value) ? n1Value : 0;
                break;
            case "N2":
                this.N2 = decimal.TryParse(value, out var n2Value) ? n2Value : 0;
                break;
            case "N3":
                this.N3 = decimal.TryParse(value, out var n3Value) ? n3Value : 0;
                break;
            case "N4":
                this.N4 = decimal.TryParse(value, out var n4Value) ? n4Value : 0;
                break;
            case "N5":
                this.N5 = decimal.TryParse(value, out var n5Value) ? n5Value : 0;
                break;
            case "N6":
                this.N6 = decimal.TryParse(value, out var n6Value) ? n6Value : 0;
                break;
            case "N7":
                this.N7 = decimal.TryParse(value, out var n7Value) ? n7Value : 0;
                break;
            case "N8":
                this.N8 = decimal.TryParse(value, out var n8Value) ? n8Value : 0;
                break;
            case "N9":
                this.N9 = decimal.TryParse(value, out var n9Value) ? n9Value : 0;
                break;
            case "N10":
                this.N10 = decimal.TryParse(value, out var n10Value) ? n10Value : 0;
                break;
            case "D1":
                this.D1 = DateTime.TryParse(value, out var d1Value) ? d1Value : null;
                break;
            case "D2":
                this.D2 = DateTime.TryParse(value, out var d2Value) ? d2Value : null;
                break;
            case "D3":
                this.D3 = DateTime.TryParse(value, out var d3Value) ? d3Value : null;
                break;
            case "D4":
                this.D4 = DateTime.TryParse(value, out var d4Value) ? d4Value : null;
                break;
            case "D5":
                this.D5 = DateTime.TryParse(value, out var d5Value) ? d5Value : null;
                break;
            case "D6":
                this.D6 = DateTime.TryParse(value, out var d6Value) ? d6Value : null;
                break;
            case "D7":
                this.D7 = DateTime.TryParse(value, out var d7Value) ? d7Value : null;
                break;
            case "D8":
                this.D8 = DateTime.TryParse(value, out var d8Value) ? d8Value : null;
                break;
            case "D9":
                this.D9 = DateTime.TryParse(value, out var d9Value) ? d9Value : null;
                break;
            case "D10":
                this.D10 = DateTime.TryParse(value, out var d10Value) ? d10Value : null;
                break;
            default:
                this.HZ = value;
                break;
        }

    }

}
