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

public class Equipment_TempEquip1 : Equipment
{
    public Equipment_TempEquip1()
        : base("�ӽ� ���1", "Equipment_TempEquip1", 1, 1, 0, -1)
    {
        this.description = "������ ���� + 1\u000a���� ���� + 1\u000a��Ȥ ���� - 1";
    }
}

public class Equipment_TempEquip2 : Equipment
{
    public Equipment_TempEquip2() 
        : base("�ӽ� ���2", "Equipment_TempEquip2", 0, 1, 0, -1)
    {
        this.description = "���� ���� + 1\u000a��Ȥ ���� - 1";
    }
}

public class Equipment_TempEquip3 : Equipment
{
    public Equipment_TempEquip3()
        : base("�ӽ� ���3", "Equipment_TempEquip3", 3, -1, -1, -1)
    {
        this.description = "������ ���� + 3\u000a���� ���� -1\u000a���� ���� - 1\u000a��Ȥ ���� -1\u000a";
    }
}
