using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueSystem : MonoBehaviour
{
    public Text dialogueText;
    public Image option1Image;
    public Image option2Image;
    public Text option1Text;
    public Text option2Text;

    private int currentDialogueIndex = 0;
    private string[] dialogueOptions = {
        "Ало, Леха, можешь сейчас говорить?",
        "ага, все равно... у меня тут свободная ночь нарисовалась",
        "го погоняем погонять по ночной Москве?",
        "Боже, хахаахах, ты опять за старое",
        "есть вариант поехать на Кутуз, или на Батайском",
        "ну так вот, жду тебя на нашем месте"
    };

    private string[] option1Responses = {
        "ахх, не очень, занят калымагой своей (((",
        "Ох, мне бы не светиться там, после прошлого то раза ...",
        "на мое состояние не смотри. кутуз"
    };

    private string[] option2Responses = {
        "валяй, я все равно откисаю пока",
        "аЭгьТааа",
        "когда мне это мешало ? Батайский !"
    };

    private void Awake()
    {
        dialogueText.text = dialogueOptions[currentDialogueIndex];
        HideOptions();
    }

    private void Start()
    {
        AddEventTriggerListener(option1Image.gameObject, EventTriggerType.PointerClick, SelectOption1);
        AddEventTriggerListener(option2Image.gameObject, EventTriggerType.PointerClick, SelectOption2);
    }

    private void AddEventTriggerListener(GameObject obj, EventTriggerType eventType, System.Action action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((eventData) => action.Invoke());
        trigger.triggers.Add(entry);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (option1Image.gameObject.activeSelf || option2Image.gameObject.activeSelf)
            {
                return;
            }

            ChangeDialogue();
        }
    }

    private void ChangeDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex >= dialogueOptions.Length)
        {
            currentDialogueIndex = 0;
        }

        dialogueText.text = dialogueOptions[currentDialogueIndex];
        CheckOptions(currentDialogueIndex);
    }

    private void CheckOptions(int dialogueIndex)
    {
        HideOptions();

        if (dialogueIndex == 0 || dialogueIndex == 2 || dialogueIndex == 4)
        {
            int optionIndex = dialogueIndex / 2;
            option1Text.text = option1Responses[optionIndex];
            option2Text.text = option2Responses[optionIndex];
            option1Image.gameObject.SetActive(true);
            option2Image.gameObject.SetActive(true);
        }
    }

    private void HideOptions()
    {
        option1Image.gameObject.SetActive(false);
        option2Image.gameObject.SetActive(false);
    }

    public void SelectOption1()
    {
        int optionIndex = GetOptionIndexFromDialogueIndex(currentDialogueIndex);
        if (optionIndex >= 0)
        {
            //dialogueText.text = option1Responses[optionIndex];
            HideOptions();
        }
    }

    public void SelectOption2()
    {
        int optionIndex = GetOptionIndexFromDialogueIndex(currentDialogueIndex);
        if (optionIndex >= 0)
        {
            //dialogueText.text = option2Responses[optionIndex];
            HideOptions();
        }
    }

    private int GetOptionIndexFromDialogueIndex(int dialogueIndex)
    {
        int optionIndex = -1;

        if (dialogueIndex == 0 || dialogueIndex == 2 || dialogueIndex == 4)
        {
            optionIndex = dialogueIndex / 2;
        }

        return optionIndex;
    }
}