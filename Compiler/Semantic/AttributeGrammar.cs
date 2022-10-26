using System.Security.Cryptography.X509Certificates;
using Compiler.Tokens;
using static Compiler.Semantic.ErrorCode;
using static Compiler.Semantic.ScopeAnalyzer;

namespace Compiler.Semantic;

public class AttributeGrammar {
    #region Rules
    public const int TYPE_IDD_EQUALS_ARRAY_NT_NUM_OF_TP_RULE = 5;
    public const int TYPE_IDD_EQUALS_STRUCT_NB_DC_RULE = 6;
    public const int TYPE_IDD_EQUALS_TP_RULE = 7;
    public const int INTEGER_RULE = 8;
    public const int CHAR_RULE = 9;
    public const int BOOLEAN_RULE = 10;
    public const int STRING_RULE = 11;
    public const int TP_IDU_RULE = 12;
    public const int DC_LI_TP_RULE = 13;
    public const int LI_TP_RULE = 14;
    public const int FUNCTION_IDD_NB_LP_TP_B_RULE = 15;
    public const int LP_IDD_TP_RULE = 16;
    public const int IDD_TP_RULE = 17;
    public const int LDV_LS_RULE = 18;
    public const int LDV_DV_RULE = 19;
    public const int DV_RULE = 20;
    public const int LS_S_RULE = 21;
    public const int S_RULE = 22;
    public const int VAR_LI_TP_RULE = 23;
    public const int LI_IDD_RULE = 24;
    public const int IDD_RULE = 25;
    public const int IF_E_S_RULE = 26;
    public const int IF_E_S_ELSE_S_RULE = 27;
    public const int WHILE_E_S_RULE = 28;
    public const int DO_S_WHILE_E_RULE = 29;
    public const int B_RULE = 30;
    public const int LV_EQUALS_E_RULE = 31;
    public const int BREAK_RULE = 32;
    public const int CONTINUE_RULE = 33;
    public const int E_AND_L_RULE = 34;
    public const int E_OR_L_RULE = 35;
    public const int L_RULE = 36;
    public const int L_LESS_THAN_R_RULE = 37;
    public const int L_GREATER_THAN_R_RULE = 38;
    public const int L_LESS_OR_EQUAL_R_RULE = 39;
    public const int L_GREATER_OR_EQUAL_R_RULE = 40;
    public const int L_EQUAL_EQUAL_R_RULE = 41;
    public const int L_NOT_EQUAL_R_RULE = 42;
    public const int R_RULE = 43;
    public const int R_PLUS_TM_RULE = 44;
    public const int R_MINUS_TM_RULE = 45;
    public const int TM_RULE = 46;
    public const int TM_TIMES_F_RULE = 47;
    public const int TM_DIVIDE_F_RULE = 48;
    public const int F_RULE = 49;
    public const int LV_RULE = 50;
    public const int PLUS_PLUS_LV_RULE = 51;
    public const int MINUS_MINUS_LV_RULE = 52;
    public const int LV_PLUS_PLUS_RULE = 53;
    public const int LV_MINUS_MINUS_RULE = 54;
    public const int F_E_RULE = 55;
    public const int IDU_LE_RULE = 56;
    public const int MINUS_F_RULE = 57;
    public const int NOT_F_RULE = 58;
    public const int NT_TRUE_RULE = 59;
    public const int NT_FALSE_RULE = 60;
    public const int NT_CHR_RULE = 61;
    public const int NT_STR_RULE = 62;
    public const int NT_NUM_RULE = 63;
    public const int LE_COMMA_E_RULE = 64;
    public const int LE_E_RULE = 65;
    public const int LV_DOT_ID_RULE = 66;
    public const int LV_E_RULE = 67;
    public const int LV_IDU_RULE = 68;
    public const int IDD_IDENTIFIER_RULE = 69;
    public const int IDU_IDENTIFIER_RULE = 70;
    public const int ID_IDENTIFIER_RULE = 71;
    public const int TRUE_RULE = 72;
    public const int FALSE_RULE = 73;
    public const int CHARACTER_RULE = 74;
    public const int STRINGVAL_RULE = 75;
    public const int NUMERAL_RULE = 76;
    public const int NB_RULE = 77;
    public const int MF_RULE = 78;
    public const int MC_RULE = 79;
    #endregion

    public static ObjectStruct s_int_ = new ObjectStruct {eKind = t_kind.SCALAR_TYPE_, nName = -1, pNext = null};
    public static ObjectStruct s_char_ = new ObjectStruct {eKind = t_kind.SCALAR_TYPE_, nName = -1, pNext = null};
    public static ObjectStruct s_bool_ = new ObjectStruct {eKind = t_kind.SCALAR_TYPE_, nName = -1, pNext = null};
    public static ObjectStruct s_string_ = new ObjectStruct {eKind = t_kind.SCALAR_TYPE_, nName = -1, pNext = null};
    public static ObjectStruct s_universal_ = new ObjectStruct {eKind = t_kind.SCALAR_TYPE_, nName = -1, pNext = null};
    public static string GeneratedCode = "";
    public static void SemanticAnalysis(int rule, TokenIdentifier token) {
        #region Prop
        var StackSem = new Stack<t_attrib>();
        int name, n;
        ObjectStruct p = new(), t = new(), f = new(), t1 = new(), t2 = new();
        t_attrib IDD_ = new();
        t_attrib IDU_ = new();
        t_attrib ID_ = new();
        t_attrib TP_ = new();
        t_attrib LI_ = new();
        t_attrib LI0_ = new();
        t_attrib LI1_ = new();
        t_attrib TRUE_ = new();
        t_attrib FALSE_ = new();
        t_attrib CHR_ = new();
        t_attrib STR_ = new();
        t_attrib NUM_ = new();
        t_attrib DC_ = new();
        t_attrib DC0_ = new();
        t_attrib DC1_ = new();
        t_attrib LP_ = new();
        t_attrib LP0_ = new();
        t_attrib LP1_ = new();
        t_attrib S_ = new();
        t_attrib E_ = new();
        t_attrib S0_ = new();
        t_attrib S1_ = new();
        t_attrib L_ = new();
        t_attrib E1_ = new();
        t_attrib E0_ = new();
        t_attrib L0_ = new();
        t_attrib L1_ = new();
        t_attrib R_ = new();
        t_attrib TM_ = new();
        t_attrib F_ = new();
        t_attrib LV_ = new();
        t_attrib F0_ = new();
        t_attrib F1_ = new();
        t_attrib LV0_ = new();
        t_attrib LV1_ = new();
        t_attrib MC_ = new();
        t_attrib LE_ = new();
        t_attrib LE0_ = new();
        t_attrib LE1_ = new();
        t_attrib R0_ = new();
        t_attrib R1_ = new();
        t_attrib TM0_ = new();
        t_attrib TM1_ = new();
        if (token == null) return;

        var tokenSecundario = token.SecondaryToken;
        #endregion
        
        switch (rule) {

            case IDD_IDENTIFIER_RULE:
                IDD_.nont = t_nterm.IDD;
                name = tokenSecundario ?? -1;
                IDD_._.ID.name = name;
                if ((p = Search(name)) != default)
                    Error(ERR_REDCL);
                else
                    p = Define(name);
                p.eKind = t_kind.NO_KIND_DEF_;
                IDD_._.ID.obj = p;
                StackSem.Push(IDD_);

                break;

            case IDU_IDENTIFIER_RULE:
                IDU_.nont = t_nterm.IDU;
                name = tokenSecundario ?? -1;
                IDU_._.ID.name = name;
                if ((p = Find(name)) == default) {
                    Error(ERR_NO_DECL);
                    p = Define(name);
                }

                IDU_._.ID.obj = p;
                StackSem.Push(IDU_);
                break;

            case ID_IDENTIFIER_RULE:
                ID_.nont = t_nterm.ID;
                name = tokenSecundario ?? -1;
                ID_._.ID.name = name;
                ID_._.ID.obj = default;
                StackSem.Push(ID_);
                break;

            case NB_RULE:
                NewBlock();
                break;

            case INTEGER_RULE:
                TP_.nont = t_nterm.TP;
                TP_._.T.type = s_int_;
                StackSem.Push(TP_);
                break;

            case CHAR_RULE:
                TP_.nont = t_nterm.TP;
                TP_._.T.type = s_char_;
                StackSem.Push(TP_);
                break;

            case BOOLEAN_RULE:
                TP_.nont = t_nterm.TP;
                TP_._.T.type = s_bool_;
                StackSem.Push(TP_);

                break;

            case STRING_RULE:
                TP_.nont = t_nterm.TP;
                TP_._.T.type = s_string_;
                StackSem.Push(TP_);

                break;

            case TP_IDU_RULE:
                IDU_ = StackSem.Peek();
                StackSem.Pop();
                p = IDU_._.ID.obj;
                if (IsTypeKind(p.eKind) || p.eKind == t_kind.UNIVERSAL_) {
                    TP_._.T.type = p;
                } else {
                    TP_._.T.type = s_universal_;
                    Error(ERR_TYPE_EXPECTED);
                }

                TP_.nont = t_nterm.TP;
                StackSem.Push(TP_);

                break;

            case IDD_RULE:
                IDD_ = StackSem.Peek();
                StackSem.Pop();
                LI_.nont = t_nterm.LI;
                LI_._.LI.list[0] = IDD_._.ID.obj;
                StackSem.Push(LI_);

                break;

            case LI_IDD_RULE:
                IDD_ = StackSem.Peek();
                StackSem.Pop();
                LI1_ = StackSem.Peek();
                StackSem.Pop();
                LI0_._.LI.list = LI1_._.LI.list;
                LI0_.nont = t_nterm.LI;
                StackSem.Push(LI0_);

                break;

            case VAR_LI_TP_RULE:
                TP_ = StackSem.Peek();
                StackSem.Pop();
                t = TP_._.T.type;
                LI_ = StackSem.Peek();
                StackSem.Pop();
                p = LI_._.LI.list[0];
                while (p != default && p.eKind == t_kind.NO_KIND_DEF_) {
                    p.eKind = t_kind.VAR_;
                    p._.Var.pType = t;
                    p = p.pNext;
                }

                break;

            case TRUE_RULE:
                TRUE_.nont = t_nterm.NT_TRUE;
                TRUE_._.BOOL.val = true;
                TRUE_._.BOOL.type = s_bool_;
                StackSem.Push(TRUE_);

                break;

            case FALSE_RULE:
                FALSE_.nont = t_nterm.NT_FALSE;
                FALSE_._.BOOL.val = false;
                FALSE_._.BOOL.type = s_bool_;
                StackSem.Push(FALSE_);

                break;

            case CHARACTER_RULE:
                CHR_.nont = t_nterm.NT_CHR;
                CHR_._.CONST.pos = tokenSecundario ?? -1;
                CHR_._.CONST.type = s_char_;
                //CHR_._.CONST.val.c = getCharConst(tokenSecundario ?? -1);
                StackSem.Push(CHR_);

                break;

            case STRINGVAL_RULE:
                STR_.nont = t_nterm.NT_STR;
                STR_._.CONST.pos = tokenSecundario ?? -1;
                STR_._.CONST.type = s_char_;
                //STR_._.CONST.val.s = new string(getStringConst(tokenSecundario ?? -1));
                StackSem.Push(STR_);
                break;

            case NUMERAL_RULE:
                NUM_.nont = t_nterm.NT_NUM;
                NUM_._.CONST.pos = tokenSecundario ?? -1;
                NUM_._.CONST.type = s_int_;
                //NUM_._.CONST.val.n = getIntConst(tokenSecundario ?? -1);
                StackSem.Push(NUM_);

                break;

            case TYPE_IDD_EQUALS_ARRAY_NT_NUM_OF_TP_RULE:
                TP_ = StackSem.Peek();
                t = TP_._.T.type;
                StackSem.Pop();

                NUM_ = StackSem.Peek();
                n = NUM_._.CONST.val.n;
                StackSem.Pop();

                IDD_ = StackSem.Peek();
                p = IDD_._.ID.obj;
                StackSem.Pop();

                p.eKind = t_kind.ARRAY_TYPE_;
                p._.Array.nNumElems = n;
                p._.Array.pElemType = t;

                break;

            case TYPE_IDD_EQUALS_TP_RULE:
                TP_ = StackSem.Peek();
                t = TP_._.T.type;
                StackSem.Pop();

                IDD_ = StackSem.Peek();
                p = IDD_._.ID.obj;
                StackSem.Pop();

                p.eKind = t_kind.ALIAS_TYPE_;
                p._.Alias.pBaseType = t;

                break;

            case TYPE_IDD_EQUALS_STRUCT_NB_DC_RULE:
                DC_ = StackSem.Peek();
                StackSem.Pop();

                IDD_ = StackSem.Peek();
                p = IDD_._.ID.obj;
                StackSem.Pop();

                p.eKind = t_kind.STRUCT_TYPE_;
                p._.Struct.pFields = DC_._.DC.list;

                EndBlock();

                break;

            case LI_TP_RULE:
                TP_ = StackSem.Peek();
                t = TP_._.T.type;
                StackSem.Pop();

                LI_ = StackSem.Peek();
                p = LI_._.LI.list[0];
                StackSem.Pop();

                while (p != default && p.eKind == t_kind.NO_KIND_DEF_) {
                    p.eKind = t_kind.FIELD_;
                    p._.Field.pType = t;
                    p = p.pNext;
                }

                DC_._.DC.list = LI_._.LI.list;
                DC_.nont = t_nterm.DC;
                StackSem.Push(DC_);

                break;

            case DC_LI_TP_RULE:
                TP_ = StackSem.Peek();
                t = TP_._.T.type;
                StackSem.Pop();

                LI_ = StackSem.Peek();
                p = LI_._.LI.list[0];
                StackSem.Pop();

                DC1_ = StackSem.Peek();
                StackSem.Pop();

                while (p != default && p.eKind == t_kind.NO_KIND_DEF_) {
                    p.eKind = t_kind.FIELD_;
                    p._.Field.pType = t;
                    p = p.pNext;
                }

                DC0_.nont = t_nterm.DC;
                DC0_._.DC.list = DC1_._.DC.list;

                StackSem.Push(DC0_);

                break;

            case IDD_TP_RULE:
                TP_ = StackSem.Peek();
                t = TP_._.T.type;
                StackSem.Pop();

                IDD_ = StackSem.Peek();
                p = IDD_._.ID.obj;
                StackSem.Pop();

                p.eKind = t_kind.PARAM_;
                p._.Param.pType = t;

                LP_._.LP.list[0] = p;
                LP_._.LP.list[0].pNext = default;
                LP_._.LP.nSize = 1;
                LP_.nont = t_nterm.LP;

                StackSem.Push(LP_);

                break;

            case LP_IDD_TP_RULE:
                TP_ = StackSem.Peek();
                t = TP_._.T.type;
                StackSem.Pop();

                IDD_ = StackSem.Peek();
                p = IDD_._.ID.obj;
                StackSem.Pop();

                LP1_ = StackSem.Peek();
                StackSem.Pop();

                p.eKind = t_kind.PARAM_;
                p._.Param.pType = t;

                LP0_._.LP.list[0] = p;
                LP0_._.LP.list[0].pNext = LP1_._.LP.list[0];
                LP0_._.LP.nSize = LP1_._.LP.nSize + 1;
                LP0_.nont = t_nterm.LP;

                StackSem.Push(LP_);

                break;

            case MF_RULE:
                TP_ = StackSem.Peek();
                StackSem.Pop();

                LP_ = StackSem.Peek();
                StackSem.Pop();

                IDD_ = StackSem.Peek();
                f = IDD_._.ID.obj;
                StackSem.Pop();

                f.eKind = t_kind.FUNCTION_;
                f._.Function.pRetType = TP_._.T.type;
                f._.Function.pParams = LP_._.LP.list;
                f._.Function.nParams = LP_._.LP.nSize;
                GeneratedCode += ($"BEGIN_FUNC {f._.Function.pParams} {f._.Function.nParams} \n");
                break;

            case FUNCTION_IDD_NB_LP_TP_B_RULE:

                EndBlock();

                break;
            case B_RULE:
                EndBlock();
                GeneratedCode += ("END_FUNC\n");
                break;
            case IF_E_S_RULE:
                E_ = StackSem.Peek();
                t = E_._.E.type;

                if (!CheckTypes(t, s_bool_))
                    Error(ERR_BOOL_TYPE_EXPECTED);

                break;

            case IF_E_S_ELSE_S_RULE:
                E_ = StackSem.Peek();
                t = E_._.E.type;

                if (!CheckTypes(t, s_bool_))
                    Error(ERR_BOOL_TYPE_EXPECTED);

                break;

            case WHILE_E_S_RULE:
                E_ = StackSem.Peek();
                t = E_._.E.type;

                if (!CheckTypes(t, s_bool_))
                    Error(ERR_BOOL_TYPE_EXPECTED);

                break;

            case DO_S_WHILE_E_RULE:
                E_ = StackSem.Peek();
                t = E_._.E.type;

                if (!CheckTypes(t, s_bool_))
                    Error(ERR_BOOL_TYPE_EXPECTED);

                break;

            case E_AND_L_RULE:
                L_ = StackSem.Peek();
                if (!CheckTypes(L_._.L.type, s_bool_))
                    Error(ERR_BOOL_TYPE_EXPECTED);
                StackSem.Pop();

                E1_ = StackSem.Peek();
                if (!CheckTypes(E1_._.E.type, s_bool_))
                    Error(ERR_BOOL_TYPE_EXPECTED);
                StackSem.Pop();

                E0_._.E.type = s_bool_;
                E0_.nont = t_nterm.E;

                StackSem.Push(E0_);

                break;

            case E_OR_L_RULE:
                L_ = StackSem.Peek();
                if (!CheckTypes(L_._.L.type, s_bool_))
                    Error(ERR_BOOL_TYPE_EXPECTED);
                StackSem.Pop();

                E1_ = StackSem.Peek();
                if (!CheckTypes(E1_._.E.type, s_bool_))
                    Error(ERR_BOOL_TYPE_EXPECTED);
                StackSem.Pop();

                E0_._.E.type = s_bool_;
                E0_.nont = t_nterm.E;

                StackSem.Push(E0_);
                GeneratedCode += "\tOR" + "\n";
                break;

            case L_RULE:
                L_ = StackSem.Peek();
                E_._.E.type = L_._.L.type;
                StackSem.Pop();

                E_.nont = t_nterm.E;
                StackSem.Push(E_);

                break;

            case L_LESS_THAN_R_RULE:
                R_ = StackSem.Peek();
                StackSem.Pop();
                L1_ = StackSem.Peek();
                StackSem.Pop();

                if (!CheckTypes(L1_._.L.type, R_._.R.type))
                    Error(ERR_TYPE_MISMATCH);
                L0_._.L.type = s_bool_;
                L0_.nont = t_nterm.L;
                StackSem.Push(L0_);
                GeneratedCode += "\tLT" + "\n";

                break;

            case L_GREATER_THAN_R_RULE:
                R_ = StackSem.Peek();
                StackSem.Pop();
                L1_ = StackSem.Peek();
                StackSem.Pop();

                if (!CheckTypes(L1_._.L.type, R_._.R.type))
                    Error(ERR_TYPE_MISMATCH);
                L0_._.L.type = s_bool_;
                L0_.nont = t_nterm.L;
                StackSem.Push(L0_);
                GeneratedCode += "\tGT" + "\n";
                break;

            case L_LESS_OR_EQUAL_R_RULE:
                R_ = StackSem.Peek();
                StackSem.Pop();
                L1_ = StackSem.Peek();
                StackSem.Pop();

                if (!CheckTypes(L1_._.L.type, R_._.R.type))
                    Error(ERR_TYPE_MISMATCH);
                L0_._.L.type = s_bool_;
                L0_.nont = t_nterm.L;
                StackSem.Push(L0_);
                GeneratedCode += "\tLE" + "\n";
                break;

            case L_GREATER_OR_EQUAL_R_RULE:
                R_ = StackSem.Peek();
                StackSem.Pop();
                L1_ = StackSem.Peek();
                StackSem.Pop();

                if (!CheckTypes(L1_._.L.type, R_._.R.type))
                    Error(ERR_TYPE_MISMATCH);
                L0_._.L.type = s_bool_;
                L0_.nont = t_nterm.L;
                StackSem.Push(L0_);
                GeneratedCode += "\tGE" + "\n";
                break;

            case L_EQUAL_EQUAL_R_RULE:
                R_ = StackSem.Peek();
                StackSem.Pop();
                L1_ = StackSem.Peek();
                StackSem.Pop();

                if (!CheckTypes(L1_._.L.type, R_._.R.type))
                    Error(ERR_TYPE_MISMATCH);
                L0_._.L.type = s_bool_;
                L0_.nont = t_nterm.L;
                StackSem.Push(L0_);
                GeneratedCode += "\tEQ" + "\n";
                break;

            case L_NOT_EQUAL_R_RULE:
                R_ = StackSem.Peek();
                StackSem.Pop();
                L1_ = StackSem.Peek();
                StackSem.Pop();

                if (!CheckTypes(L1_._.L.type, R_._.R.type))
                    Error(ERR_TYPE_MISMATCH);
                L0_._.L.type = s_bool_;
                L0_.nont = t_nterm.L;
                StackSem.Push(L0_);
                GeneratedCode += "\tNE" + "\n";
                break;

            case R_RULE:
                R_ = StackSem.Peek();
                L_._.L.type = R_._.R.type;
                StackSem.Pop();

                L_.nont = t_nterm.L;
                StackSem.Push(L_);

                break;

            case R_PLUS_TM_RULE:
                TM_ = StackSem.Peek();
                StackSem.Pop();

                R1_ = StackSem.Peek();
                StackSem.Pop();

                if (!CheckTypes(TM_._.TM.type, R1_._.R.type))
                    Error(ERR_TYPE_MISMATCH);
                if (!CheckTypes(R1_._.R.type, s_int_) && !CheckTypes(R1_._.R.type, s_string_))
                    Error(ERR_INVALID_TYPE);

                R0_._.R.type = R1_._.R.type;
                R0_.nont = t_nterm.R;
                StackSem.Push(R0_);
                GeneratedCode += "\tADD" + "\n";
                break;

            case R_MINUS_TM_RULE:
                TM_ = StackSem.Peek();
                StackSem.Pop();

                R1_ = StackSem.Peek();
                StackSem.Pop();

                if (!CheckTypes(TM_._.TM.type, R1_._.R.type))
                    Error(ERR_TYPE_MISMATCH);
                if (!CheckTypes(R1_._.R.type, s_int_))
                    Error(ERR_INVALID_TYPE);

                R0_._.R.type = R1_._.R.type;
                R0_.nont = t_nterm.R;
                StackSem.Push(R0_);
                GeneratedCode += "\tSUB" + "\n";
                break;

            case TM_RULE:
                TM_ = StackSem.Peek();
                R_._.R.type = TM_._.TM.type;
                StackSem.Pop();

                R_.nont = t_nterm.R;
                StackSem.Push(R_);

                break;

            case TM_TIMES_F_RULE:
                F_ = StackSem.Peek();
                StackSem.Pop();

                TM1_ = StackSem.Peek();
                StackSem.Pop();

                if (!CheckTypes(TM1_._.TM.type, F_._.F.type))
                    Error(ERR_TYPE_MISMATCH);
                if (!CheckTypes(TM1_._.TM.type, s_int_) && !CheckTypes(TM1_._.R.type, s_string_))
                    Error(ERR_INVALID_TYPE);

                TM0_._.TM.type = TM1_._.TM.type;
                TM0_.nont = t_nterm.TM;
                StackSem.Push(TM0_);
                GeneratedCode += "\tMUL" + "\n";
                break;

            case TM_DIVIDE_F_RULE:
                F_ = StackSem.Peek();
                StackSem.Pop();

                TM_ = StackSem.Peek();
                StackSem.Pop();

                if (!CheckTypes(TM_._.TM.type, F_._.F.type))
                    Error(ERR_TYPE_MISMATCH);
                if (!CheckTypes(TM_._.TM.type, s_int_) && !CheckTypes(TM_._.R.type, s_string_))
                    Error(ERR_INVALID_TYPE);

                TM0_._.TM.type = TM1_._.TM.type;
                TM0_.nont = t_nterm.TM;
                StackSem.Push(TM0_);
                GeneratedCode += "\tDIV" + "\n";
                break;

            case F_RULE:
                F_ = StackSem.Peek();
                TM_._.TM.type = F_._.F.type;
                StackSem.Pop();

                TM_.nont = t_nterm.TM;
                StackSem.Push(TM_);

                break;

            case LV_RULE:
                LV_ = StackSem.Peek();
                F_._.F.type = LV_._.LV.type;
                StackSem.Pop();

                F_.nont = t_nterm.F;
                StackSem.Push(F_);
                GeneratedCode += $"\tDE_REF {F_.nont} \n";
                break;

            case PLUS_PLUS_LV_RULE:
                LV_ = StackSem.Peek();
                t = LV_._.LV.type;
                StackSem.Pop();

                if (!CheckTypes(t, s_int_))
                    Error(ERR_INVALID_TYPE);

                F_._.F.type = s_int_;
                F_.nont = t_nterm.F;
                StackSem.Push(F_);
                GeneratedCode += "\tDUP\n\tDUP\n\tDE_REF 1" + "\n";
                GeneratedCode += "\tINC\n\tSTORE REF 1\n\tDE_REF 1" + "\n";
                break;

            case MINUS_MINUS_LV_RULE:
                LV_ = StackSem.Peek();
                t = LV_._.LV.type;
                StackSem.Pop();

                if (!CheckTypes(t, s_int_))
                    Error(ERR_INVALID_TYPE);

                F_._.F.type = s_int_;
                F_.nont = t_nterm.F;
                StackSem.Push(F_);
                GeneratedCode += "\tDUP\n\tDUP\n\tDE_REF 1" + "\n";
                GeneratedCode += "\tDEC\n\tSTORE_REF 1\n\tDE_REF 1" + "\n";
                break;

            case LV_PLUS_PLUS_RULE:
                LV_ = StackSem.Peek();
                t = LV_._.LV.type;
                StackSem.Pop();

                if (!CheckTypes(t, s_int_))
                    Error(ERR_INVALID_TYPE);

                F_._.F.type = s_int_;
                F_.nont = t_nterm.F;
                StackSem.Push(F_);
                GeneratedCode += ("\tDUP\n\tDUP\n\tDE_REF 1" + "\n");
                GeneratedCode += ("\tINC\n\tSTORE_REF 1\n\tDE_REF 1" + "\n");
                GeneratedCode += ("\tDEC" + "\n");

                break;

            case LV_MINUS_MINUS_RULE:
                LV_ = StackSem.Peek();
                t = LV_._.LV.type;
                StackSem.Pop();

                if (!CheckTypes(t, s_int_))
                    Error(ERR_INVALID_TYPE);

                F_._.F.type = s_int_;
                F_.nont = t_nterm.F;
                StackSem.Push(F_);
                GeneratedCode += ("\tDUP\n\tDUP\n\tDE_REF 1" + "\n");
                GeneratedCode += ("\tDEC\n\tSTORE_REF 1\n\tDE_REF 1" + "\n");
                GeneratedCode += ("\tINC" + "\n");

                break;

            case MINUS_F_RULE:
                F1_ = StackSem.Peek();
                t = F1_._.F.type;
                StackSem.Pop();

                if (!CheckTypes(t, s_int_))
                    Error(ERR_INVALID_TYPE);

                F0_._.F.type = s_int_;
                F0_.nont = t_nterm.F;
                StackSem.Push(F0_);
                GeneratedCode += ("\tNEG" + "\n");
                break;

            case NOT_F_RULE:
                F1_ = StackSem.Peek();
                t = F1_._.F.type;
                StackSem.Pop();

                if (!CheckTypes(t, s_bool_))
                    Error(ERR_INVALID_TYPE);

                F0_._.F.type = s_int_;
                F0_.nont = t_nterm.F;
                StackSem.Push(F0_);
                GeneratedCode += ("\tNOT" + "\n");
                break;

            case NT_TRUE_RULE:
                TRUE_ = StackSem.Peek();
                StackSem.Pop();

                F_._.F.type = s_bool_;
                F_.nont = t_nterm.F;
                StackSem.Push(F_);
                GeneratedCode += "\tLOAD_TRUE" + "\n";
                break;

            case NT_FALSE_RULE:
                FALSE_ = StackSem.Peek();
                StackSem.Pop();

                F_._.F.type = s_bool_;
                F_.nont = t_nterm.F;
                StackSem.Push(F_);
                GeneratedCode += "\tLOAD_FALSE" + "\n";
                break;

            case NT_CHR_RULE:
                CHR_ = StackSem.Peek();
                StackSem.Pop();

                F_._.F.type = s_char_;
                F_.nont = t_nterm.F;
                StackSem.Push(F_);
                GeneratedCode += ($"\tLOAD_CONST {F_.nont}\n");
                break;

            case NT_STR_RULE:
                STR_ = StackSem.Peek();
                StackSem.Pop();

                F_._.F.type = s_string_;
                F_.nont = t_nterm.F;
                StackSem.Push(F_);

                break;

            case NT_NUM_RULE:
                NUM_ = StackSem.Peek();
                StackSem.Pop();

                F_._.F.type = s_int_;
                F_.nont = t_nterm.F;
                StackSem.Push(F_);

                break;

            case LV_DOT_ID_RULE:
                ID_ = StackSem.Peek();
                StackSem.Pop();

                LV1_ = StackSem.Peek();
                t = LV1_._.LV.type;
                StackSem.Pop();

                if (t.eKind != t_kind.STRUCT_TYPE_) {
                    if (t.eKind != t_kind.UNIVERSAL_)
                        Error(ERR_KIND_NOT_STRUCT);
                    LV0_._.LV.type = s_universal_;
                } else {
                    p = t._.Struct.pFields[0];
                    while (p != default) {
                        if (p.nName == ID_._.ID.name) break;

                        p = p.pNext;
                    }

                    if (p == default) {
                        Error(ERR_FIELD_NOT_DECL);
                        LV0_._.LV.type = s_universal_;
                    } else {
                        LV0_._.LV.type = p._.Field.pType;
                    }
                }

                LV0_.nont = t_nterm.LV;
                StackSem.Push(LV0_);

                break;

            case LV_E_RULE:
                E_ = StackSem.Peek();
                StackSem.Pop();

                LV1_ = StackSem.Peek();
                t = LV1_._.LV.type;
                StackSem.Pop();

                if (t == s_string_) {
                    LV0_._.LV.type = s_char_;
                } else if (t.eKind != t_kind.ARRAY_TYPE_) {
                    if (t.eKind != t_kind.UNIVERSAL_)
                        Error(ERR_KIND_NOT_ARRAY);
                    LV0_._.LV.type = s_universal_;
                } else {
                    LV0_._.LV.type = t._.Array.pElemType;
                }

                if (!CheckTypes(E_._.E.type, s_int_))
                    Error(ERR_INVALID_INDEX_TYPE);

                LV0_.nont = t_nterm.LV;
                StackSem.Push(LV0_);

                break;

            case LV_IDU_RULE:
                IDU_ = StackSem.Peek();
                p = IDU_._.ID.obj;
                StackSem.Pop();

                if (p.eKind != t_kind.VAR_ && p.eKind != t_kind.PARAM_) {
                    if (p.eKind != t_kind.UNIVERSAL_) Error(ERR_KIND_NOT_VAR);
                    LV_._.LV.type = s_universal_;
                } else {
                    LV_._.LV.type = p._.Var.pType;
                }

                LV_.nont = t_nterm.LV;
                StackSem.Push(LV_);

                break;
            case LE_E_RULE:
                E_ = StackSem.Peek();
                StackSem.Pop();
                MC_ = StackSem.Peek();

                LE_._.LE.param = default;
                LE_._.LE.err = MC_._.MC.err;
                n = 1;
                if (!MC_._.MC.err) {
                    p = MC_._.MC.param;
                    if (p == default) {
                        Error(ERR_TOO_MANY_ARGS);
                        LE_._.LE.err = true;
                    } else {
                        if (!CheckTypes(p._.Param.pType, E_._.E.type))
                            Error(ERR_PARAM_TYPE);
                        LE_._.LE.param = p.pNext;
                        LE_._.LE.n = n + 1;
                    }
                }

                LE_.nont = t_nterm.LE;
                StackSem.Push(LE_);

                break;

            case IDU_LE_RULE:
                LE_ = StackSem.Peek();
                StackSem.Pop();
                MC_ = StackSem.Peek();
                StackSem.Pop();
                IDU_ = StackSem.Peek();
                StackSem.Pop();

                F_._.F.type = MC_._.MC.type;
                if (!MC_._.MC.err) {
                    if (LE_._.LE.n - 1 < f._.Function.nParams && LE_._.LE.n != 0)
                        Error(ERR_TOO_FEW_ARGS);
                    else if (LE_._.LE.n - 1 > f._.Function.nParams)
                        Error(ERR_TOO_MANY_ARGS);
                }

                F_.nont = t_nterm.F;
                StackSem.Push(F_);

                break;

            case LE_COMMA_E_RULE:
                E_ = StackSem.Peek();
                StackSem.Pop();
                LE1_ = StackSem.Peek();
                StackSem.Pop();
                LE0_._.LE.param = default;
                LE0_._.LE.err = L1_._.LE.err;

                n = LE1_._.LE.n;
                if (!LE1_._.LE.err) {
                    p = LE1_._.LE.param;
                    if (p == default) {
                        Error(ERR_TOO_MANY_ARGS);
                        LE0_._.LE.err = true;
                    } else {
                        if (!CheckTypes(p._.Param.pType, E_._.E.type))
                            Error(ERR_PARAM_TYPE);
                        LE0_._.LE.param = p.pNext;
                        LE0_._.LE.n = n + 1;
                    }
                }

                LE0_.nont = t_nterm.LE;
                StackSem.Push(LE0_);

                break;

            case MC_RULE:
                IDU_ = StackSem.Peek();
                f = IDU_._.ID.obj;

                if (f.eKind != t_kind.FUNCTION_) {
                    Error(ERR_KIND_NOT_FUNCTION);
                    MC_._.MC.type = s_universal_;
                    MC_._.MC.param = default;
                    MC_._.MC.err = true;
                } else {
                    MC_._.MC.type = f._.Function.pRetType;
                    MC_._.MC.param = f._.Function.pParams[0];
                    MC_._.MC.err = false;
                }

                MC_.nont = t_nterm.MC;
                StackSem.Push(MC_);

                break;

            case LV_EQUALS_E_RULE:
                E_ = StackSem.Peek();
                t1 = E_._.E.type;
                StackSem.Pop();

                LV_ = StackSem.Peek();
                t2 = LV_._.LV.type;
                StackSem.Pop();

                if (!CheckTypes(t1, t2))
                    Error(ERR_TYPE_MISMATCH);

                F_._.F.type = t2;
                F_.nont = t_nterm.F;

                break;

            default:
                break;
        }
    }

    private static void Error(ErrorCode code) {
        switch (code) {
            case ERR_NO_DECL:
                throw new Exception("Variavel nao declarada");
            case ERR_REDCL:
                throw new Exception("Variavel ja foi declarada");
            case ERR_TYPE_EXPECTED:
                throw new Exception("Type Expected: Um tipo nao foi declarado anteriormente");
            case ERR_BOOL_TYPE_EXPECTED:
                throw new Exception("Bool Expected: Um tipo booleano e esperado para expressao");
            case ERR_INVALID_TYPE:
                throw new Exception("Invalid Type: O tipo e invalido para a operacao");
            case ERR_TYPE_MISMATCH:
                throw new Exception("Type Mismatch: O tipo e invalido para a operacao");
            case ERR_KIND_NOT_STRUCT:
                throw new Exception("Kind not Struct: A operacao so pode ser realizada em tipos Struct");
            case ERR_FIELD_NOT_DECL:
                throw new Exception("Field not Declared: O campo nao foi declarado na estrutura");
            case ERR_KIND_NOT_ARRAY:
                throw new Exception("Kind not Array: A operacao so pode ser realizada para um Array");
            case ERR_INVALID_INDEX_TYPE:
                throw new Exception("Invalid Index: O Indice especificado para o Array e invalido");
            case ERR_KIND_NOT_VAR:
                throw new Exception("Kind not Var: A operacao so e valida com tipos Var");
            case ERR_KIND_NOT_FUNCTION:
                throw new Exception("Kind not Function: A operacao so e valida com tipos Function");
            case ERR_TOO_FEW_ARGS:
                throw new Exception("Too Few Args: O numero de parametros especificado nao e suficiente");
            case ERR_TOO_MANY_ARGS:
                throw new Exception("Too Many Args: O numero de parametros especificado e maior que o especificado");
            case ERR_PARAM_TYPE:
                throw new Exception("Param Type: O tipo especificado para o parametro e invalido");
        }
    }

    public static bool CheckTypes(ObjectStruct t1, ObjectStruct t2) {
        if (t1 == t2) {
            return true;
        } else if (t1 == s_universal_ || t2 == s_universal_) {
            return true;
        } else if (t1.eKind == t_kind.UNIVERSAL_ || t2.eKind == t_kind.UNIVERSAL_) {
            return true;
        } else if (t1.eKind == t_kind.ALIAS_TYPE_ && t2.eKind != t_kind.ALIAS_TYPE_) {
            return CheckTypes(t1._.Alias.pBaseType, t2);
        } else if (t1.eKind != t_kind.ALIAS_TYPE_ && t2.eKind == t_kind.ALIAS_TYPE_) {
            return CheckTypes(t1, t2._.Alias.pBaseType);
        } else if (t1.eKind == t2.eKind) {
            if (t1.eKind == t_kind.ALIAS_TYPE_) {
                return CheckTypes(t1._.Alias.pBaseType, t2._.Alias.pBaseType);
            } else if (t1.eKind == t_kind.ARRAY_TYPE_) {
                if (t1._.Array.nNumElems == t2._.Array.nNumElems)
                    return CheckTypes(t1._.Array.pElemType, t2._.Array.pElemType);
            } else if (t1.eKind == t_kind.STRUCT_TYPE_) {
                var f1 = t1._.Struct.pFields;
                var f2 = t2._.Struct.pFields;

                while (f1 != null && f2 != null)
                    if (!CheckTypes(f1[0]._.Field.pType, f2[0]._.Field.pType))
                        return false;

                return (f1 == null && f2 == null);
            }
        }

        return false;
    }

    public static bool IsTypeKind(t_kind k) => (k == t_kind.ARRAY_TYPE_ ||
                                                k == t_kind.STRUCT_TYPE_ ||
                                                k == t_kind.ALIAS_TYPE_ ||
                                                k == t_kind.SCALAR_TYPE_);
}

public enum ErrorCode {
    ERR_NO_DECL = 1,
    ERR_REDCL,
    ERR_TYPE_EXPECTED,
    ERR_BOOL_TYPE_EXPECTED,
    ERR_INVALID_TYPE,
    ERR_TYPE_MISMATCH,
    ERR_KIND_NOT_STRUCT,
    ERR_FIELD_NOT_DECL,
    ERR_KIND_NOT_ARRAY,
    ERR_INVALID_INDEX_TYPE,
    ERR_KIND_NOT_VAR,
    ERR_KIND_NOT_FUNCTION,
    ERR_TOO_FEW_ARGS,
    ERR_TOO_MANY_ARGS,
    ERR_PARAM_TYPE
}

public class ObjectStruct {
    public int nName { get; set; }
    public ObjectStruct pNext { get; set; }
    public t_kind eKind { get; set; }
    public ObjectFields _ { get; set; } = new();
}

public class ObjectFields {
    public VarStruct Var { get; set; } = new();
    public ParamStruct Param { get; set; } = new();
    public FieldStruct Field { get; set; } = new();
    public FunctionStruct Function { get; set; } = new();
    public ArrayStruct Array { get; set; } = new();
    public StructStruct Struct { get; set; } = new();
    public AliasStruct Alias { get; set; } = new();
    public TypeStruct Type { get; set; } = new();
}

public class StructStruct {
    public ObjectStruct[] pFields { get; set; }
}

public class AliasStruct {
    public ObjectStruct pBaseType { get; set; }
}

public class TypeStruct {
    public ObjectStruct pBaseType { get; set; }
}

public class VarStruct {
    public ObjectStruct pType { get; set; }
}

public class ParamStruct {
    public ObjectStruct pType { get; set; }
}

public class FieldStruct {
    public ObjectStruct pType { get; set; }
}

public class FunctionStruct {
    public ObjectStruct pRetType { get; set; }
    public ObjectStruct[] pParams { get; set; }
    public int nParams { get; set; }
}

public class ArrayStruct {
    public ObjectStruct pElemType { get; set; }
    public int nNumElems { get; set; }
}

public class t_attrib {
    public t_nterm nont { get; set; } = new();
    public t_attrib__ _ { get; set; } = new();

    public class t_attrib__ {
        public class t_attrib_id {
            public ObjectStruct obj { get; set; }
            public int name { get; set; }
        }

        public t_attrib_id ID { get; set; } = new();

        public class t_attrib_TELRTMFLV {
            public ObjectStruct type { get; set; }
        }

        public t_attrib_TELRTMFLV T { get; set; } = new();
        public t_attrib_TELRTMFLV E { get; set; } = new();
        public t_attrib_TELRTMFLV L { get; set; } = new();
        public t_attrib_TELRTMFLV R { get; set; } = new();
        public t_attrib_TELRTMFLV TM { get; set; } = new();
        public t_attrib_TELRTMFLV F { get; set; } = new();
        public t_attrib_TELRTMFLV LV { get; set; } = new();

        public class t_attrib_LIDC {
            public ObjectStruct[] list { get; set; }
        }

        public t_attrib_LIDC LI = new();
        public t_attrib_LIDC DC = new();

        public class t_attrib_LP {
            public ObjectStruct[] list { get; set; }
            public int nSize { get; set; }
        }

        public t_attrib_LP LP = new();

        public class t_attrib_BOOL {
            public ObjectStruct type { get; set; }
            public bool val { get; set; }
        }

        public t_attrib_BOOL BOOL = new();

        public class t_attrib_MC {
            public ObjectStruct type { get; set; }
            public ObjectStruct param { get; set; }
            public bool err { get; set; }
        }

        public t_attrib_MC MC { get; set; } = new();

        public class t_attrib_LE {
            public ObjectStruct type { get; set; } = new();
            public ObjectStruct param { get; set; } = new();
            public bool err { get; set; }
            public int n { get; set; }
        }

        public t_attrib_LE LE { get; set; } = new();

        public class t_attrib_CONST {
            public ObjectStruct type { get; set; } = new();
            public int pos { get; set; }

            public class t_attrib_CONST_val {
                public int n { get; set; }
                public char c { get; set; }
                public string s { get; set; }
            }

            public t_attrib_CONST_val val { get; set; } = new();
        }

        public t_attrib_CONST CONST { get; set; } = new();
    }
}

public enum t_kind {
    NO_KIND_DEF_ = -1,
    VAR_,
    PARAM_,
    FUNCTION_,
    FIELD_,
    ARRAY_TYPE_,
    STRUCT_TYPE_,
    ALIAS_TYPE_,
    SCALAR_TYPE_,
    UNIVERSAL_
}

public enum t_nterm {
    P = 51,
    LDE,
    DE,
    DT,
    TP,
    DC,
    DF,
    LP,
    B,
    LDV,
    LS,
    DV,
    LI,
    S,
    E,
    L,
    R,
    TM,
    F,
    LE,
    LV,
    IDD,
    IDU,
    ID,
    NT_TRUE,
    NT_FALSE,
    NT_CHR,
    NT_STR,
    NT_NUM,
    NB,
    MF,
    MC
}