using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum COOK_TYPE { HEAT, CHOPPING, MIX, FLASK }

public class RecipeCreator : MonoBehaviour {

    public int currStep = 0;
    public int maxSteps = 5;
    public int maxAmmountOfIngredients = 2;
    public RecipeStep[] recipeSteps;
    public Ingredient[] posibleIngrediens;

    public int failSteps = 0;
    public int maxFailSteps = 3;

    // UI ----
    public GameObject recipeParent;
    public GameObject recipeStepUI;
    public GameObject ingredientEntryUI;

    // Singleton
    private static RecipeCreator instance;
    public static RecipeCreator Instance { get { return instance; } }

    public void generateRecipe()
    {
        bool mixed = false;
        for(int i = 0; i < maxSteps - 1; i++)
        {
            int ran;

            if (i > 1)
            {
                ran = Random.Range(0, 2);
                if (ran == 1)
                {
                    // MIX
                    mixed = true;
                    RecipeStep mix = new RecipeStep(COOK_TYPE.MIX);
                    recipeSteps[i] = mix;
                }
            }

            if(!mixed)
            {
                ran = Random.Range(0, 2);
                switch (ran)
                {
                    case 0:
                        // HEAT
                        RecipeStep heat = new RecipeStep(getRandomIngredients(maxAmmountOfIngredients), COOK_TYPE.HEAT);
                        recipeSteps[i] = heat;
                        break;
                    case 1:
                        // CHOPPING
                        RecipeStep chopping = new RecipeStep(getRandomIngredients(maxAmmountOfIngredients), COOK_TYPE.CHOPPING);
                        recipeSteps[i] = chopping;
                        break;
                }
            }

            mixed = false;
        }

        RecipeStep flask = new RecipeStep(COOK_TYPE.FLASK);
        recipeSteps[maxSteps - 1] = flask;
    }

    public void generateUI()
    {
        int stepNum = 1;
        foreach(RecipeStep step in recipeSteps)
        {
            GameObject uiElement = Instantiate(recipeStepUI, recipeParent.transform) as GameObject;
            uiElement.transform.Find("Text").GetComponent<Text>().text = stepNum + ".";
            stepNum++;

            if(step.action == COOK_TYPE.HEAT || step.action == COOK_TYPE.CHOPPING)
            {
                Transform ingredientParent = uiElement.transform.Find("Items");
                foreach (Ingredient i in step.ingredients)
                {
                    GameObject ingElement = Instantiate(ingredientEntryUI, ingredientParent) as GameObject;
                    ingElement.GetComponent<Image>().sprite = i.sprite;
                    ingElement.name = i.ingredient_name;
                }
            }
            
        }
    }

    private Ingredient[] getRandomIngredients(int ammount)
    {
        int rand = Random.Range(1, ammount + 1);
    
        Ingredient[] list = new Ingredient[rand];

        int randIngredient = Random.Range(0, posibleIngrediens.Length);
        for(int i = 0; i < rand; i++)
        {
            list[i] = posibleIngrediens[randIngredient];
            randIngredient = Random.Range(0, posibleIngrediens.Length);

            // Debug.Log("Ingr: " + posibleIngrediens[randIngredient].ingredient_name + " Rand: " + rand);
        }

        return list;
    }

    public void ReceiveInformation(COOK_TYPE type, string information)
    {
        RecipeStep currentStep = recipeSteps[currStep];
        // Check if the current step action is equl to the the given type, if not add strike
        if (currentStep.action != type)
        {
            failSteps++;

            if (failSteps >= maxFailSteps)
            {
                Debug.Log("End");
            }

            return;
        }

        switch (type)
        {
            case COOK_TYPE.HEAT:
                if(string.IsNullOrEmpty(information))
                {
                    Debug.LogWarning("No Information given");
                    return;
                }

                foreach(Ingredient i in currentStep.ingredients)
                {
                    if(i.ingredient_name.Equals(information))
                    {
                        Transform checkmark = recipeParent.transform.GetChild(currStep).GetChild(1).Find(information).GetChild(0);
                        if(checkmark.gameObject.activeSelf)
                        {
                            checkmark = recipeParent.transform.GetChild(currStep).GetChild(1).Find(information).GetChild(1);
                            checkmark.gameObject.SetActive(true);
                        } else
                        {
                            checkmark.gameObject.SetActive(true);
                        }
                    }
                }
                break;
            case COOK_TYPE.CHOPPING:

                break;
            case COOK_TYPE.FLASK:

                break;
            case COOK_TYPE.MIX:

                break;

        }

        checkForCompletion();
    }

    private bool checkForCompletion()
    {
        RecipeStep currentStep = recipeSteps[currStep];
        int count = 0;
        Transform checkmark = recipeParent.transform.GetChild(currStep).Find("Items");
        for(int i = 0; i < checkmark.childCount; i++)
        {
            if(checkmark.GetChild(i).GetChild(0).gameObject.activeSelf)
            {
                count++;
            }
        }

        if (count == currentStep.ingredients.Length)
        {
            Transform finished = recipeParent.transform.GetChild(currStep).Find("Finish");
            finished.gameObject.SetActive(true);
            currStep++;

            // Add Win
            if(currStep > maxSteps)
            {

            }
            return true;
        }
        else return false;
    }


    // Use this for initialization
    private void Start()
    {
        // if (maxSteps > 0) recipeSteps = new RecipeStep[maxSteps];

        // generateRecipe();
        recipeSteps = new RecipeStep[1];
        RecipeStep heat = new RecipeStep(getRandomIngredients(maxAmmountOfIngredients), COOK_TYPE.HEAT);
        recipeSteps[0] = heat;
        generateUI();
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}

[System.Serializable]
public struct RecipeStep
{
    public Ingredient[] ingredients;
    public COOK_TYPE action;

    public RecipeStep(Ingredient[] ingredients, COOK_TYPE action)
    {
        this.ingredients = ingredients;
        this.action = action;
    }

    public RecipeStep(COOK_TYPE action)
    {
        this.ingredients = null;
        this.action = action;
    }
}
