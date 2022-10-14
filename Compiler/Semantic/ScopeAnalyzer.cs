namespace Compiler.Semantic;

public static class ScopeAnalyzer {
    public static List<ObjectStruct> SymbolTable { get; set; }
    public static List<ObjectStruct> SymbolTableLast { get; set; }
    public static int nCurrentLevel { get; set; }

    public static int NewBlock() {
        SymbolTable[++nCurrentLevel] = null;
        SymbolTableLast[nCurrentLevel] = null;
        return nCurrentLevel;
    }

    public static int EndBlock() {
        return --nCurrentLevel;
    }

    public static ObjectStruct Define(int aName) {
        ObjectStruct obj = default;
        obj.nName = aName;
        obj.pNext = null;

        if (SymbolTable[nCurrentLevel] == null) {
            SymbolTable[nCurrentLevel] = obj;
            SymbolTableLast[nCurrentLevel] = obj;
        } else {
            SymbolTableLast[nCurrentLevel].pNext = obj;
            SymbolTableLast[nCurrentLevel] = obj;
        }

        return obj;
    }

    public static ObjectStruct Search(int aName) {
        var obj = SymbolTable[nCurrentLevel];
        while (obj != null)
            if (obj.nName == aName)
                break;
            else
                obj = obj.pNext;

        return obj;
    }

    public static ObjectStruct Find(int aName) {
        int i;
        ObjectStruct obj = default;
        for (i = nCurrentLevel; i >= 0; --i) {
            obj = SymbolTable[i];
            while (obj != null)
                if (obj.nName == aName)
                    break;
                else
                    obj = obj.pNext;

            if (obj != null) break;
        }

        return obj;
    }
}