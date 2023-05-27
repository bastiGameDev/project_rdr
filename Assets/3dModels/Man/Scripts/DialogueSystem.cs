using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    public Text dialogueText;
    public Image option1Image;
    public Image option2Image;
    public Text option1Text;
    public Text option2Text;
    public AudioSource audioSource; // Ссылка на компонент AudioSource

    private bool isTyping = false;

    private int currentDialogueIndex = 0;
    private string[] dialogueOptions = {
        "Ало, Леха, можешь сейчас говорить?",
        "ага, все равно... у меня тут вечер освободился",
        "го погоняем по ночной Москве?",
        "Боже, хахаахах, ты опять за старое, отоспись сначала",
        "Не бойся, это будет наша маленькая секретная операция. Куда выбираешься: Кутуз или Батайский?",
        "ну так вот, жду тебя на нашем месте"
    };

    private string[] option1Responses = {
        "ахх, не очень, занят калымагой своей (((",
        "Ох, мне бы не светиться там, после прошлого то раза ... Но что там, я всегда готов показать, что я настоящий дорожный хам!",
        "На мое состояние не смотри. Кутуз! Я там покажу всем, что правила дорожного движения - это не для меня!"
    };

    private string[] option2Responses = {
        "валяй, я все равно не занят",
        "аЭ-гьТ.ааа",
        "Когда мне это мешало? Батайский! Я всегда готов показать свою мастерскую по нарушению всех правил!"
    };

    private Color originalColorOption1;
    private Color originalColorOption2;

    private Coroutine typingCoroutine; // Ссылка на запущенную корутину печати

    private void Awake()
    {
        HideOptions();
        dialogueText.text = dialogueOptions[currentDialogueIndex];
        CheckOptions(currentDialogueIndex);
    }

    private void Start()
    {
        AddEventTriggerListener(option1Image.gameObject, EventTriggerType.PointerClick, SelectOption1);
        AddEventTriggerListener(option2Image.gameObject, EventTriggerType.PointerClick, SelectOption2);

        originalColorOption1 = option1Image.color;
        originalColorOption2 = option2Image.color;

        AddHoverEvents(option1Image.gameObject, Option1HoverEnter, Option1HoverExit);
        AddHoverEvents(option2Image.gameObject, Option2HoverEnter, Option2HoverExit);
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
            if (option1Image.gameObject.activeSelf || option2Image.gameObject.activeSelf || isTyping)
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

        // Остановка предыдущей корутины печати, если она была запущена
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        isTyping = true; // Установка состояния печати в true

        // Запуск новой корутины печати
        typingCoroutine = StartCoroutine(TypeText(dialogueOptions[currentDialogueIndex]));

        // После завершения печати показываем опции
        StartCoroutine(ShowOptionsAfterTyping());
    }

    private IEnumerator ShowOptionsAfterTyping()
    {
        while (isTyping)
        {
            yield return null;
        }

        CheckOptions(currentDialogueIndex);
    }


    private IEnumerator TypeText(string text)
    {
        dialogueText.text = ""; // Очистка текста перед печатью

        for (int i = 0; i < text.Length; i++)
        {
            dialogueText.text += text[i]; // Печать очередного символа

            yield return new WaitForSeconds(0.05f); // Задержка между символами
        }

        isTyping = false; // Установка состояния печати в false после завершения печати текста
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
            dialogueText.text = option1Responses[optionIndex];
            HideOptions();

            // Воспроизведение звука
            audioSource.PlayOneShot(audioSource.clip);

            ChangeDialogue();
        }
    }

    public void SelectOption2()
    {
        int optionIndex = GetOptionIndexFromDialogueIndex(currentDialogueIndex);
        if (optionIndex >= 0)
        {
            dialogueText.text = option2Responses[optionIndex];
            HideOptions();

            // Воспроизведение звука
            audioSource.PlayOneShot(audioSource.clip);

            ChangeDialogue();
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

    private void AddHoverEvents(GameObject obj, System.Action hoverEnterAction, System.Action hoverExitAction)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) => hoverEnterAction.Invoke());
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) => hoverExitAction.Invoke());
        trigger.triggers.Add(entryExit);
    }

    private void Option1HoverEnter()
    {
        option1Image.color = new Color(originalColorOption1.r, originalColorOption1.g, originalColorOption1.b, 0.5f);
    }

    private void Option1HoverExit()
    {
        option1Image.color = originalColorOption1;
    }

    private void Option2HoverEnter()
    {
        option2Image.color = new Color(originalColorOption2.r, originalColorOption2.g, originalColorOption2.b, 0.5f);
    }

    private void Option2HoverExit()
    {
        option2Image.color = originalColorOption2;
    }
}