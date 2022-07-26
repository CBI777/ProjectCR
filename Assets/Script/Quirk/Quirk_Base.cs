using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Quirk_Base
{
    public string quirkName;
    public string realName;
    public string quirkDescription = "�׷�... �̰��� �����߱�... �׷��� ���� ���� �ƴϴ�.";
    //true�� ���� will, false�� ���� feebleness
    public bool isWill;

    public static event Action<int[]> QuirkResistChangeEvent;
    public static event Action<int> TokenChangeEvent;

    public Quirk_Base(string quirkName, string realName,  bool isWill)
    {
        this.quirkName = quirkName;
        this.realName = realName;
        this.isWill = isWill;
    }

    public virtual void onObtain() { }
    public virtual void onBattleStart() { }
    public virtual void onTurnStart() { }
    public virtual void onTurnEnd() { }
    public virtual void onBattleEnd() { }
    public virtual void onLose() { }

    public void changeResist(int p, int f, int a, int d)
    {
        QuirkResistChangeEvent?.Invoke(new int[4] { p, f, a, d });
    }

    public void changeToken(int n)
    {
        TokenChangeEvent?.Invoke(n);
    }
}

public class Quirk_BasicUpPhys : Quirk_Base
{
    public Quirk_BasicUpPhys()
        : base("ưư�� ��ü", "Quirk_BasicUpPhys", true)
    {
        this.quirkDescription = "������ ���� ���� + 1";
    }

    public override void onObtain()
    {
        changeResist(1, 0, 0, 0);
    }

    public override void onLose()
    {
        changeResist(-1, 0, 0, 0);
    }
}

public class Quirk_BasicUpFear : Quirk_Base
{
    public Quirk_BasicUpFear()
        : base("������ ����", "Quirk_BasicUpFear", true)
    {
        this.quirkDescription = "���� ���� + 1";
    }

    public override void onObtain()
    {
        changeResist(0, 1, 0, 0);
    }

    public override void onLose()
    {
        changeResist(0, -1, 0, 0);
    }
}

public class Quirk_BasicUpAbhorr : Quirk_Base
{
    public Quirk_BasicUpAbhorr()
        : base("�ܸ�", "Quirk_BasicUpAbhorr", true)
    {
        this.quirkDescription = "���� ���� + 1";
    }

    public override void onObtain()
    {
        
        changeResist(0, 0, 1, 0);
    }

    public override void onLose()
    {
        changeResist(0, 0, -1, 0);
    }
}

public class Quirk_BasicUpDelus : Quirk_Base
{
    public Quirk_BasicUpDelus()
        : base("������", "Quirk_BasicUpDelus", true)
    {
        this.quirkDescription = "��Ȥ ���� + 1";
    }

    public override void onObtain()
    {
        changeResist(0, 0, 0, 1);
    }

    public override void onLose()
    {
        changeResist(0, 0, 0, -1);
    }
}

public class Quirk_BasicDownPhys : Quirk_Base
{
    public Quirk_BasicDownPhys()
        : base("����", "Quirk_BasicDownPhys", false)
    {
        this.quirkDescription = "������ ���� ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(-1, 0, 0, 0);
    }

    public override void onLose()
    {
        changeResist(1, 0, 0, 0);
    }
}

public class Quirk_BasicDownFear : Quirk_Base
{
    public Quirk_BasicDownFear()
        : base("���� ����", "Quirk_BasicDownFear", false)
    {
        this.quirkDescription = "���� ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(0, -1, 0, 0);
    }

    public override void onLose()
    {
        changeResist(0, 1, 0, 0);
    }
}

public class Quirk_BasicDownAbhorr : Quirk_Base
{
    public Quirk_BasicDownAbhorr()
        : base("���� ����", "Quirk_BasicDownAbhorr", false)
    {
        this.quirkDescription = "���� ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(0, 0, -1, 0);
    }

    public override void onLose()
    {
        changeResist(0, 0, 1, 0);
    }
}

public class Quirk_BasicDownDelus : Quirk_Base
{
    public Quirk_BasicDownDelus()
        : base("ȣ���", "Quirk_BasicDownDelus", false)
    {
        this.quirkDescription = "��Ȥ ���� - 1";
    }

    public override void onObtain()
    {
        changeResist(0, 0, 0, -1);
    }

    public override void onLose()
    {
        changeResist(0, 0, 0, 1);
    }
}

public class Quirk_TokenDownOne : Quirk_Base
{
    public Quirk_TokenDownOne()
        : base("������ ������", "Quirk_TokenDownOne", false)
    {
        this.quirkDescription = "�� �� ��� ��ū�� �� �� ���� ����";
    }

    public override void onObtain()
    {
        changeToken(-1);
    }
    public override void onLose()
    {
        changeToken(1);
    }
}

public class Quirk_TokenUpOne : Quirk_Base
{
    public Quirk_TokenUpOne()
        : base("�žӽ�", "Quirk_TokenUpOne", false)
    {
        this.quirkDescription = "�� �� ��� ��ū�� �� �� �� ����";
    }

    public override void onObtain()
    {
        changeToken(1);
    }
    public override void onLose()
    {
        changeToken(-1);
    }
}