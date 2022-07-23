using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    //����� �̹����� realName���� ó��
    public string equipName;
    public string realName;
    public string description = "�װ� �̰� ���� �ִٴ� ���� ������ ������ �ʾҴٴ� ���̰���... �׷�... �׷��ǰ�...";
    public int[] resChange = new int[4];

    public Equipment(string name, string realname, int p, int f, int a, int d)
    {
        this.equipName = name;
        this.realName = realname;
        this.resChange[0] = p;
        this.resChange[1] = f;
        this.resChange[2] = a;
        this.resChange[3] = d;
    }
}

public class Equipment_Pistol : Equipment
{
    public Equipment_Pistol()
        : base("�ǽ���", "Equipment_Pistol", 0, 1, -1, 0)
    {
        this.description = "���� ���� + 1\u000a���� ���� - 1";
    }
}

public class Equipment_Lantern : Equipment
{
    public Equipment_Lantern() 
        : base("�⹦�� ���", "Equipment_Lantern", 0, -1, 0, 1)
    {
        this.description = "���� ���� - 1\u000a��Ȥ ���� + 1";
    }
}

public class Equipment_EyeballJar : Equipment
{
    public Equipment_EyeballJar()
        : base("������ �� ��", "Equipment_EyeballJar", 3, -1, -1, -1)
    {
        this.description = "������ ���� + 3\u000a���� ���� -1\u000a���� ���� - 1\u000a��Ȥ ���� -1\u000a";
    }
}

public class Equipment_Blindfold : Equipment
{
    public Equipment_Blindfold()
        : base("�� ������", "Equipment_Blindfold", -4, 1, 1, 1)
    {
        this.description = "������ ���� - 4\u000a���� ���� + 1\u000a���� ���� + 1\u000a��Ȥ ���� + 1\u000a";
    }
}
