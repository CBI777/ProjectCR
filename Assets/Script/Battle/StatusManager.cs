using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StatusManager : MonoBehaviour
{
    [SerializeField] private int sanity; //�̼�. �Ѱ� ���Ϸ� �پ��� ���� ���ظ�ŭ ����� ������ �ȴ�.
    [SerializeField] private int sanityMax; //�̼��� �ִ�ġ. �̼��� ���� �پ��� ���⼭���� �ٽý���.
    [SerializeField] private int sanityMin; //�̼��� �Ѱ�.
    [SerializeField] private int sanityBoundary; //�̼��� �������� ���� �ּ� ��ġ

    [SerializeField] private int madness; //����. �ִ�ġ�� �Ѿ�� �Ǹ� ���Ĺ�����.
    [SerializeField] private int madnessMax; //������ �ִ�ġ.
    [SerializeField] private int obsession; //(� ����) ����. �Ѱ� �̻����� ���̰� �Ǹ� ������ ������ �Ǿ������.
    [SerializeField] private int obsessionMax;

    [SerializeField] private TextMeshProUGUI sanityText;
    [SerializeField] private TextMeshProUGUI madnessText;
    [SerializeField] private TextMeshProUGUI obsessionText;
    [SerializeField] private Slider SanitySlider;
    [SerializeField] private Slider MadnessSlider;
    [SerializeField] private Slider ObsessionSlider;
    [SerializeField] private Image SanityBG;
    [SerializeField] private Image SanityBar;
    [SerializeField] private Image MadnessBar;
    [SerializeField] private Image ObsessionBar;

    //�⺻������ ���� �߿��� �ϴ� ó���� �� ����, �ϰ� �� ���̿��� Ȯ���� �ϰ� �߰����� ���𰡸� �ϵ��� �Ǿ��ֱ� ������...
    //�ϴ� sanity�� �ٴ��� ���ų�, obsession�� �� ���ų�, Madness�� �� ���� '�ϴ���' �ƹ� ������ ���� ������ ��.

    //Obsession�� ��Ʋ ����ÿ� ó���� �� �ٰŰ�, Sanity�� Madness�� �ϰ� �� ���̿��� ���÷� üũ.
    //��, turn���� manager���� �ִ� action�� ������ ó���� �ȴٴ°���. supply�� �� �ϵ� �ϴ� ����� ����.
    //�̰� skill�� �ϳ��ϳ� ���鼭 ó���� �� �� ���� ����� ����.

    public static event Action<int> SanityEmptyEvent; //UI�ʿ��� �ް���? ��... �Ҹ��� ������...
    public static event Action<int> MadnessFullEvent; //���� ������ ���õ� ���̰� ���� event
    public static event Action<int> ObsessionFullEvent; //UI�ʰ� �⺮�ʿ��� �ް���?

    private void OnEnable()
    {
        Supply_Base.SupplySanityChange += SanityChange;
        Supply_Base.SupplyMadnessChange += MadnessChange;
        Supply_Base.SupplyObsessionChange += ObsessionChange;
    }

    private void OnDisable()
    {
        Supply_Base.SupplySanityChange -= SanityChange;
        Supply_Base.SupplyMadnessChange -= MadnessChange;
        Supply_Base.SupplyObsessionChange -= ObsessionChange;
    }


    private void ObsessionChange(int n)
    {
        this.obsession += n;

        if (this.obsession > obsessionMax) { this.obsession = obsessionMax; this.ObsessionBar.color = new Color(1f, 80f / 255f, 50f / 255f); }
        else { this.ObsessionBar.color = new Color(1f, 185f/255f, 75/255f); }
        
        if (this.obsession <= 0) { this.obsession = 0; this.ObsessionBar.gameObject.SetActive(false); }
        else { if (!this.ObsessionBar.gameObject.activeSelf) { this.ObsessionBar.gameObject.SetActive(true); } }

        this.obsessionText.SetText(this.obsession + " / " + obsessionMax);
        this.ObsessionSlider.value = ((float)this.obsession / obsessionMax);
    }
    private void SanityChange(int n)
    {
        this.sanity -= n;

        if (this.sanity < sanityMin) { this.sanity = sanityMin; }
        else if (this.sanity > sanityMax) { this.sanity = sanityMax; }

        if (this.sanity <= 0)
        { 
            this.SanityBar.gameObject.SetActive(false);
            this.SanityBG.color = new Color(1f, (125 + this.sanity) / 255f, (125 + this.sanity) / 255f);
        }
        else
        {
            this.SanityBG.color = Color.white;
            if (!this.SanityBar.gameObject.activeSelf) { this.SanityBar.gameObject.SetActive(true); }
            this.SanitySlider.value = ((float)this.sanity / sanityMax);
        }

        this.sanityText.SetText(this.sanity + " / " + sanityMax);
    }
    private void MadnessChange(int n)
    {
        this.madness += n;
        if (this.madness > madnessMax) { this.madness = madnessMax; }

        if (this.madness <= 0) { this.madness = 0; this.MadnessBar.gameObject.SetActive(false); }
        else { if (!this.MadnessBar.gameObject.activeSelf) { this.MadnessBar.gameObject.SetActive(true); } }

        this.madnessText.SetText(this.madness + " / " + madnessMax);

        float temp = ((float)this.madness / madnessMax);
        this.MadnessSlider.value = temp;
        this.MadnessBar.color = new Color((200 - (70 * temp)) / 255f, (150 - (150 * temp)) / 255f, 1f);
    }

    private void Start()
    {
        this.madnessMax = 100;
        this.sanityMax = 100;
        this.obsessionMax = 100;
        this.sanityMin = -50;
        this.sanity = 100;
        this.madness = 0;
        this.obsession = 0;

        SanityChange(80);
        MadnessChange(40);
        ObsessionChange(30);
    }
}
