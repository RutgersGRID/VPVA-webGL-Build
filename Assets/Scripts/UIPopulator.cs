using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPopulator : MonoBehaviour
{
    public DCSVtoSO dcsvToSO;
    public JCSVtoSO jcsvToSO;
    int currentIndex = 0;
    private VisualElement dlogElements;
    private VisualElement dlogBG;
    private VisualElement propImage;
    private VisualElement characterImageLeft;
    private VisualElement characterImageRight;
    private VisualElement rewindUI;
    private Button rewind;
    private Button rewindYes;
    private Button rewindNo;
    private Button journal;
    ///
    private VisualElement DBox;
    private TextElement nameText;
    private TextElement dialogueText;
    private Button nextButton;
    ///
    private VisualElement twoOptionAnswers;
    private TextElement questionTwoAnswers;
    private TextElement textATwo;
    private Button aTwo;
    private TextElement textBTwo;
    private Button bTwo;
    ///
    private VisualElement threeOptionAnswers;
    private TextElement questionThreeAnswers;
    private TextElement textAThree;
    private Button aThree;
    private TextElement textBThree;
    private Button bThree;
    private TextElement textCThree;
    private Button cThree;
    ///
    private VisualElement journalUIContainer;
    private TextElement journalEntry;
    private TextElement journalTitle;
    private TextElement journalQuestion;
    private Button journalExit;
    private Button nextPage;
    private Button previousPage;
    private Button reflectionPage;
    private Button aboutPage;
    private Button howToPlayPage;
    private VisualElement reflectionPageContainer;
    private VisualElement aboutPageContainer;
    private VisualElement howToPlayPageContainer;
    private Button bookmarkOne;
    private Button bookmarkTwo;
    private Button bookmarkThree;
    private Button bookmarkFour;
    private Button bookmarkFive;

    ///
    private Button jEButton;
    private VisualElement NewJournalEntry;
    private TextElement Title;
    private TextElement SummaryText;
    private TextElement ReflectionQ;  
    private TextElement Question; 

    public AudioClip newLogClip;
    public AudioClip checkpointClip;
    public AudioClip pageflipClip;
    public AudioClip dialogueBeepClip;
    AudioSource Audio;
    private int previousCheckPoint;
    string testText;
    int pageNumber = 0;
    List<string> jEventText = new List<string>();
    List<string> jEventTitle = new List<string>();
    List<string> jEventQuestionText = new List<string>();
    List<int> jPages = new List<int>();
    public int jNumber = 0;
    int[] pages;
    string[] pageLog;

    public float fadeDuration = 1.0f;
    Sprite nextCharacter;
    Sprite currentCharacter;
    Sprite leftCharacter;
    
 private void Start()
    {
        Audio = GetComponent<AudioSource>();
        var root = GetComponent<UIDocument>().rootVisualElement;
        dlogElements = root.Q<VisualElement>("dlog-elements");
        dlogBG = root.Q<VisualElement>("dlog-bg");
        characterImageLeft = root.Q<VisualElement>("CharacterLeft");
        characterImageRight = root.Q<VisualElement>("CharacterRight");
        propImage = root.Q<VisualElement>("Props");
        rewindUI = root.Q<VisualElement>("Rewind");

        DBox = root.Q<VisualElement>("DBox");
        nameText = root.Q<TextElement>("DialogueName");
        dialogueText = root.Q<TextElement>("DialogueText");

        rewind = root.Q<Button>("rewind-button");
        rewindYes = root.Q<Button>("rewind-yes");
        rewindNo = root.Q<Button>("rewind-no");
        nextButton = root.Q<Button>("dlog-button");

        twoOptionAnswers = root.Q<VisualElement>("answer-two-options-bg");
        questionTwoAnswers = root.Q<TextElement>("question-text-two");
        aTwo = root.Q<Button>("button-a-two");
        bTwo = root.Q<Button>("button-b-two");

        threeOptionAnswers = root.Q<VisualElement>("answer-three-options-bg");
        questionThreeAnswers = root.Q<TextElement>("question-text-three");
        aThree = root.Q<Button>("button-a-three");
        bThree = root.Q<Button>("button-b-three");
        cThree = root.Q<Button>("button-c-three");

        journal = root.Q<Button>("journal");
        //journalUIContainer = root.Q<VisualElement>("JournalUIContainer");
        journalUIContainer = root.Q<VisualElement>("JournalUIContainerOld");
        journalEntry = root.Q<TextElement>("journalSummaryText");
        journalTitle = root.Q<TextElement>("journalTitle");
        journalQuestion = root.Q<TextElement>("journalreflectionQuestion");
        journalExit = root.Q<Button>("exit-ui-button");
        nextPage = root.Q<Button>("next-page");
        previousPage = root.Q<Button>("back-page");
        reflectionPage= root.Q<Button>("ReflectionEvents");
        aboutPage= root.Q<Button>("About");
        howToPlayPage= root.Q<Button>("HowToPlay");
        reflectionPageContainer = root.Q<VisualElement>("ReflectionEventPage");
        aboutPageContainer = root.Q<VisualElement>("AboutPage");
        howToPlayPageContainer = root.Q<VisualElement>("HowToPlayPage");
        bookmarkOne = root.Q<Button>("BookmarkOne");
        bookmarkTwo = root.Q<Button>("BookmarkTwo");
        bookmarkThree = root.Q<Button>("BookmarkThree");
        bookmarkFour = root.Q<Button>("BookmarkFour");
        bookmarkFive = root.Q<Button>("BookmarkFive");

        NewJournalEntry = root.Q<VisualElement>("NewJournalEntry");
        jEButton = root.Q<Button>("JEButton");
        Title = root.Q<TextElement>("Title");
        SummaryText = root.Q<TextElement>("SummaryText");
        ReflectionQ = root.Q<TextElement>("reflectionQ");  
        Question = root.Q<TextElement>("reflectionQuestion"); 

        nextButton.RegisterCallback<ClickEvent>(NextDialogue);
        aTwo.RegisterCallback<ClickEvent>(NextDialogueA);
        bTwo.RegisterCallback<ClickEvent>(NextDialogueB);
        aThree.RegisterCallback<ClickEvent>(NextDialogueA);
        bThree.RegisterCallback<ClickEvent>(NextDialogueB);
        cThree.RegisterCallback<ClickEvent>(NextDialogueC);

        rewind.RegisterCallback<ClickEvent>(ShowRewind);
        rewindYes.RegisterCallback<ClickEvent>(RewindYes);
        rewindNo.RegisterCallback<ClickEvent>(RewindNo);

        journal.RegisterCallback<ClickEvent>(ShowJournal);
        journalExit.RegisterCallback<ClickEvent>(ExitJournal);
        nextPage.RegisterCallback<ClickEvent>(nextPageB);
        previousPage.RegisterCallback<ClickEvent>(preivousPageB);
        reflectionPage.RegisterCallback<ClickEvent>(showReflection);
        aboutPage.RegisterCallback<ClickEvent>(showAbout);
        howToPlayPage.RegisterCallback<ClickEvent>(showHowToPlay);
        bookmarkOne.RegisterCallback<ClickEvent>(bMarkOne);
        bookmarkTwo.RegisterCallback<ClickEvent>(bMarkTwo);
        bookmarkThree.RegisterCallback<ClickEvent>(bMarkThree);
        bookmarkFour.RegisterCallback<ClickEvent>(bMarkFour);
        bookmarkFive.RegisterCallback<ClickEvent>(bMarkFive);

        jEButton.RegisterCallback<ClickEvent>(JournalEntryButton);

        rewindUI.style.display = DisplayStyle.None;
        twoOptionAnswers.style.display = DisplayStyle.None;
        threeOptionAnswers.style.display = DisplayStyle.None;
        journalUIContainer.style.display = DisplayStyle.None;
        previousPage.style.display = DisplayStyle.None;
        NewJournalEntry.style.display = DisplayStyle.None;
        howToPlayPageContainer.style.display = DisplayStyle.None;
        aboutPageContainer.style.display = DisplayStyle.None;
        bookmarkOne.style.display = DisplayStyle.None;
        bookmarkTwo.style.display = DisplayStyle.None;
        bookmarkThree.style.display = DisplayStyle.None;
        bookmarkFour.style.display = DisplayStyle.None;
        bookmarkFive.style.display = DisplayStyle.None;
        PopulateUI();
    }
    private void PopulateUI()
    {
        var dialogueSO = dcsvToSO.dialogues[currentIndex];

        dlogBG.style.backgroundImage = new StyleBackground(dialogueSO.Background);
        characterImageLeft.style.backgroundImage = new StyleBackground(dialogueSO.LeftSideSpeaker);
        characterImageRight.style.backgroundImage = new StyleBackground(dialogueSO.RightSideSpeaker);
        propImage.style.backgroundImage = new StyleBackground(dialogueSO.Prop);

        if (dialogueSO.Type.Equals("a", StringComparison.CurrentCultureIgnoreCase))
        {
            dlogBG.style.display = DisplayStyle.Flex;
            DBox.style.display = DisplayStyle.Flex;
            twoOptionAnswers.style.display = DisplayStyle.None;
            threeOptionAnswers.style.display = DisplayStyle.None;

            var dialogueSO2 = dcsvToSO.dialogues[currentIndex + 1];
            nextCharacter = dialogueSO2.LeftSideSpeaker;
            currentCharacter = dialogueSO.LeftSideSpeaker;
            leftCharacter = dialogueSO.LeftSideSpeaker;
            
            if (currentCharacter != nextCharacter)
            {
                FadeIn(characterImageLeft);
            }

            nameText.text = dialogueSO.Speaker;
            testText = dialogueSO.Line;
            ScrollText();
        }
        else if (dialogueSO.Type.Equals("b", StringComparison.CurrentCultureIgnoreCase))
        {
            dlogBG.style.display = DisplayStyle.Flex;
            DBox.style.display = DisplayStyle.None;
            twoOptionAnswers.style.display = DisplayStyle.Flex;
            threeOptionAnswers.style.display = DisplayStyle.None;

            questionTwoAnswers.text = dialogueSO.Line;
            aTwo.text = dialogueSO.A1Answer;
            bTwo.text = dialogueSO.A2Answer;
        }
        else if (dialogueSO.Type.Equals("c", StringComparison.CurrentCultureIgnoreCase))
        {
            dlogBG.style.display = DisplayStyle.Flex;
            DBox.style.display = DisplayStyle.None;
            twoOptionAnswers.style.display = DisplayStyle.None;
            threeOptionAnswers.style.display = DisplayStyle.Flex;

            questionThreeAnswers.text = dialogueSO.Line;
            aThree.text = dialogueSO.A1Answer;
            bThree.text = dialogueSO.A2Answer;
            cThree.text = dialogueSO.A3Answer;
        }
        if (dialogueSO.Effect != -1 && jNumber != 0)
        {
            if (!jPages.Contains(jNumber))
            {
                NewJournalEntry.style.display = DisplayStyle.Flex;
                //jNumber = dialogueSO.Effect;
            }
        }
        else if (dialogueSO.Effect != -1)
        {
            //if (!jPages.Contains(jNumber))
            if (!jPages.Contains(dialogueSO.Effect))
            {
                jNumber = dialogueSO.Effect;
                var journalSO = jcsvToSO.journals[jNumber];
                Title.text = journalSO.journalTitle;
                SummaryText.text = journalSO.journalEntry;
                Question.text = journalSO.reflectionQuestion;
                NewJournalEntry.style.display = DisplayStyle.Flex;
            }
        }
        else if (!jPages.Contains(jNumber) && jPages.Count>0)
        {
                NewJournalEntry.style.display = DisplayStyle.Flex;
        }
        journalUpdate();
    }
    public void FadeIn(VisualElement targetElement)
{
    // Start a coroutine to fade in the background image
    StartCoroutine(FadeInCoroutine(targetElement));
}

private System.Collections.IEnumerator FadeInCoroutine(VisualElement targetElement)
{
    // Store the original opacity value
    float originalOpacity = targetElement.style.opacity.value;

    // Calculate the increment value for each frame
    float increment = 1.0f / fadeDuration * Time.deltaTime;

    // Gradually increase the opacity value until it reaches 1
    while (targetElement.style.opacity.value < 1)
    {
        targetElement.style.opacity = targetElement.style.opacity.value + increment;
        yield return null;
    }

    // Ensure the opacity value is set to 1
    targetElement.style.opacity = 1;

    // Stop the coroutine
    yield break;
}

    // public void StartFadeOut()
    // {
    //     StartCoroutine(FadeOut());
    // }

    // private IEnumerator FadeOut()
    // {
    //     float startTime = Time.time;

    //     while (Time.time < startTime + duration)
    //     {
    //         float t = (Time.time - startTime) / duration;
    //         myElement.style.opacity = 1 - t;
    //         yield return null;
    //     }

    //     myElement.style.opacity = 0;
    // }
    private void showReflection(ClickEvent evt)
    {
        reflectionPageContainer.style.display = DisplayStyle.Flex;
        howToPlayPageContainer.style.display = DisplayStyle.None;
        aboutPageContainer.style.display = DisplayStyle.None;
    }
    private void showAbout(ClickEvent evt)
    {
        reflectionPageContainer.style.display = DisplayStyle.None;
        howToPlayPageContainer.style.display = DisplayStyle.Flex;
        aboutPageContainer.style.display = DisplayStyle.None;
    }
    private void showHowToPlay(ClickEvent evt)
    {
        reflectionPageContainer.style.display = DisplayStyle.None;
        howToPlayPageContainer.style.display = DisplayStyle.None;
        aboutPageContainer.style.display = DisplayStyle.Flex;
    }
    public void ScrollText()
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(testText));

    }
    IEnumerator TypeSentence (string sentence) 
    {   //char space = " ";
        dialogueText.text = "";
        int counter = 0;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            // if (stopAudioSource)
            // {
            //     audioSource.Stop();
            // }
            // if ((letter.Equals(" ") == false) && (counter % charInterval == 0))
            // {
            //     audioSource.PlayOneShot(dialogueTypingSoundClip);
            // }
            if (counter%4 == 0)
            {
                Audio.PlayOneShot(dialogueBeepClip, 0.7F);
            }
            
            yield return new WaitForSeconds(.02f);
            counter++;
        }
    }
    private void NextDialogue(ClickEvent evt)
    {
        var dialogueSO = dcsvToSO.dialogues[currentIndex];
        currentIndex = dialogueSO.GoToID;
        PopulateUI();
    }
    private void NextDialogueA(ClickEvent evt)
    {
        var dialogueSO = dcsvToSO.dialogues[currentIndex];
        var journalSO = jcsvToSO.journals[dialogueSO.EffectA1];
        jNumber = dialogueSO.EffectA1;

        Title.text = journalSO.journalTitle;
        SummaryText.text = journalSO.journalEntry;
        Question.text = journalSO.reflectionQuestion;

        currentIndex = dialogueSO.GoToIDA1;
        dialogueSO = dcsvToSO.dialogues[currentIndex];
        Debug.Log("jNumber " + jNumber);
        PopulateUI();
    }
    private void NextDialogueB(ClickEvent evt)
    {
        var dialogueSO = dcsvToSO.dialogues[currentIndex];
        var journalSO = jcsvToSO.journals[dialogueSO.EffectA2];
        jNumber = dialogueSO.EffectA2;

        Title.text = journalSO.journalTitle;
        SummaryText.text = journalSO.journalEntry;
        Question.text = journalSO.reflectionQuestion;

        currentIndex = dialogueSO.GoToIDA2;
        dialogueSO = dcsvToSO.dialogues[currentIndex];
        Debug.Log("jNumber " + jNumber);
        PopulateUI();
    }
    private void NextDialogueC(ClickEvent evt)
    {
        var dialogueSO = dcsvToSO.dialogues[currentIndex];
        var journalSO = jcsvToSO.journals[dialogueSO.EffectA3];
        jNumber = dialogueSO.EffectA3;

        Title.text = journalSO.journalTitle;
        SummaryText.text = journalSO.journalEntry;
        Question.text = journalSO.reflectionQuestion;

        currentIndex = dialogueSO.GoToIDA3;
        dialogueSO = dcsvToSO.dialogues[currentIndex];
        Debug.Log("jNumber " + jNumber);
        PopulateUI();
    }
    private void ShowRewind(ClickEvent evt)
    {
        rewindUI.style.display = DisplayStyle.Flex;
    }
    private void RewindYes(ClickEvent evt)
    {
        var dialogueSO = dcsvToSO.dialogues[currentIndex];
        currentIndex = dialogueSO.Checkpoint;
        rewindUI.style.display = DisplayStyle.None;
        PopulateUI();
        
    }
    private void RewindNo(ClickEvent evt)
    {
        rewindUI.style.display = DisplayStyle.None;
    }
    private void ShowJournal(ClickEvent evt)
    {
        journalUIContainer.style.display = DisplayStyle.Flex;
        
    }
    private void ExitJournal(ClickEvent evt)
    {
        journalUIContainer.style.display = DisplayStyle.None;
    }
    private void nextPageB(ClickEvent evt)
    {
        pageNumber++;
        Audio.PlayOneShot(pageflipClip, 0.7F);
        Debug.Log("Clicked Next");
        journalUpdate();


    }
    private void preivousPageB(ClickEvent evt)
    {
        pageNumber--;
        Audio.PlayOneShot(pageflipClip, 0.7F);
        Debug.Log("Clicked Previous");
        journalUpdate();

    }
    private void bMarkOne(ClickEvent evt)
    {
        pageNumber = 0;
        Audio.PlayOneShot(pageflipClip, 0.7F);
        journalUpdate();
    }
    private void bMarkTwo(ClickEvent evt)
    {
        pageNumber = 1;
        Audio.PlayOneShot(pageflipClip, 0.7F);
        journalUpdate();
    }
    private void bMarkThree(ClickEvent evt)
    {
        pageNumber = 2;
        Audio.PlayOneShot(pageflipClip, 0.7F);
        journalUpdate();
    }
    private void bMarkFour(ClickEvent evt)
    {
        pageNumber = 3;
        Audio.PlayOneShot(pageflipClip, 0.7F);
        journalUpdate();
    }
    private void bMarkFive(ClickEvent evt)
    {
        pageNumber = 4;
        Audio.PlayOneShot(pageflipClip, 0.7F);
        journalUpdate();
    }
    public void journalUpdate()
    {

        if (jPages.Count <= 0)
        {
            nextPage.style.display = DisplayStyle.None;
            previousPage.style.display = DisplayStyle.None;
        }
        
        else if (pageNumber >= 0 && pageNumber < jPages.Count)
        {
            if (pageNumber == jPages.Count - 1)
            {
                nextPage.style.display = DisplayStyle.None;
                if (jPages.Count > 1)
                {
                    previousPage.style.display = DisplayStyle.Flex;
                }
            }
            else if (pageNumber == 0)
            {
                previousPage.style.display = DisplayStyle.None;
                if (jPages.Count > 1)
                {
                    nextPage.style.display = DisplayStyle.Flex;
                }
            }
            else
            {
                nextPage.style.display = DisplayStyle.Flex;
                previousPage.style.display = DisplayStyle.Flex;
            }
            if (pageNumber < jEventText.Count)
            {
                //eventText.text = jEventText[jEventText.Count -1];
                journalEntry.text = jEventText[pageNumber];
                journalTitle.text = jEventTitle[pageNumber];
                journalQuestion.text = jEventQuestionText[pageNumber];
                Debug.Log("pageNumber: " + pageNumber + ", eventText: " + journalEntry.text);
                Debug.Log("jPages" + string.Join(", ", jPages));
                Debug.Log("jEventText" + string.Join(", ", jEventText));
            }
            else
            {
                Debug.LogError("pageNumber is out of range! jEventText count: " + jEventText.Count);
                // Handle the error, e.g., set eventText.text to an error message or disable UI elements
            }
        }
        else
        {
            Debug.LogError("pageNumber is out of range! jPages count: " + jPages.Count);
            Debug.LogError("pageNumber is out of range! pageNumber count: " + pageNumber);
            // Handle the error, e.g., set eventText.text to an error message or disable UI elements
        }
    }
    private void JournalEntryButton(ClickEvent evt)
        {
            //var dialogueSO = csvToSOTwo.dialogues[currentIndex];
            var dialogueSO = dcsvToSO.dialogues[currentIndex];
            var journalSO = jcsvToSO.journals[jNumber];

            Debug.Log("jNumber " + jNumber);
            Debug.Log("jEventText" + journalSO.journalEntry);
            Debug.Log("Page Number " + pageNumber);


            Audio.PlayOneShot(newLogClip, 0.7F);
            
            if (dialogueSO.Effect != -1 && jPages.Count < 0)
            {
                //if (!jPages.Contains(dialogueSO.Effect))
                if (!jPages.Contains(jNumber))
                {
                    jPages.Add(jNumber);
                    //jPages.Add(pageNumber);
                    jEventText.Add(journalSO.journalEntry);
                    jEventTitle.Add(journalSO.journalTitle);
                    jEventQuestionText.Add(journalSO.reflectionQuestion);
                    journalEntry.text = jEventText[pageNumber];
                    journalTitle.text = jEventTitle[pageNumber];
                    journalQuestion.text = jEventQuestionText[pageNumber];
                }
            }
            else if (!jPages.Contains(jNumber))
            {
                jEventText.Add(journalSO.journalEntry);
                jEventTitle.Add(journalSO.journalTitle);
                jEventQuestionText.Add(journalSO.reflectionQuestion);
                jPages.Add(jNumber);
                journalEntry.text = jEventText[jPages.Count -1];
                journalTitle.text = jEventTitle[jPages.Count -1];
                journalQuestion.text = jEventQuestionText[jPages.Count -1];
            } 
            Debug.Log("jNumber " + jNumber);
            Debug.Log("jEventText" + journalSO.journalEntry);
            Debug.Log("Page Number " + pageNumber);

            Debug.Log("Page Number " + pageNumber);
            Debug.Log("jPages" + string.Join(", ", jPages));

            if(jPages.Count >= 5)
            {
            bookmarkOne.style.display = DisplayStyle.Flex;
            bookmarkTwo.style.display = DisplayStyle.Flex;
            bookmarkThree.style.display = DisplayStyle.Flex;
            bookmarkFour.style.display = DisplayStyle.Flex;
            bookmarkFive.style.display = DisplayStyle.Flex;
            }
            if(jPages.Count >= 4)
            {
            bookmarkOne.style.display = DisplayStyle.Flex;
            bookmarkTwo.style.display = DisplayStyle.Flex;
            bookmarkThree.style.display = DisplayStyle.Flex;
            bookmarkFour.style.display = DisplayStyle.Flex;
            }
            if(jPages.Count >= 3)
            {
            bookmarkOne.style.display = DisplayStyle.Flex;
            bookmarkTwo.style.display = DisplayStyle.Flex;
            bookmarkThree.style.display = DisplayStyle.Flex;
            }
            if(jPages.Count >= 2)
            {
                bookmarkOne.style.display = DisplayStyle.Flex;
                bookmarkTwo.style.display = DisplayStyle.Flex;
            }
            if(jPages.Count >= 1)
            {
                bookmarkOne.style.display = DisplayStyle.Flex;
            }
            journalUpdate();
            NewJournalEntry.style.display = DisplayStyle.None;
        }
}

