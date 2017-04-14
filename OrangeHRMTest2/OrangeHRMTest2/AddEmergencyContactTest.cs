using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System;

class AddEmergencyContactTest
{
    static IWebDriver driver = new ChromeDriver();

    static string testID = "TC_IM_01";
    static string team = "05";
    static string tableRowsXPath = "//table[@id='emgcontact_list']/tbody/tr";
    static int numRows = 0;  // Will hold the number of emergency contacts


    static void Main()
    {
        // Printing team # and test ID
        PrintTeamAndTestID();

        // Login 
        Login();

        // Navigating to the emergency contact tab and adding 1 contact
        AddEmergencyContact();

        // Checks if emergency contact is added
        if (IsEmeregencyContactAdded())
            Console.WriteLine("Contact Added Successfuly!");

        // 3 seconds pause
        Thread.Sleep(3000);

        // Close the browser
        driver.Quit();
    }

    private static void Login()
    {
        IWebElement userNameInputField;
        IWebElement userPasswordInputField;
        IWebElement loginButton;

        string userName = "linda.anderson";
        string userPassword = "linda.anderson";

        string url = "http://opensource.demo.orangehrmlive.com/";
        string userNameID = "txtUsername";
        string userPasswordID = "txtPassword";
        string loginButtonID = "btnLogin";

        try
        {
            // Navigating to the main page
            driver.Navigate().GoToUrl(url);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            driver.Quit();
        }

        try
        {
            // Getting the user name input feild
            userNameInputField = driver.FindElement(By.Id(userNameID));

            // Getting the password input field
            userPasswordInputField = driver.FindElement(By.Id(userPasswordID));

            // Getting the login button
            loginButton = driver.FindElement(By.Id(loginButtonID));

            // Entering username
            userNameInputField.SendKeys(userName);

            // Entering password
            userPasswordInputField.SendKeys(userPassword);

            // Clicking login button
            loginButton.Click();
        }
        catch (NoSuchElementException e)
        {
            Console.WriteLine(e);
            driver.Quit();
        }

    }

    private static void AddEmergencyContact()
    {
        IWebElement myInfoTab;
        IWebElement emergencyContactTab;
        IWebElement addButton;
        IWebElement nameInputField;
        IWebElement relationshipInputField;
        IWebElement homeTelephoneInputField;
        IWebElement mobileInputField;
        IWebElement workTelephoneInputField;
        IWebElement saveButton;

        string myInfoTabID = "menu_pim_viewMyDetails";
        string emergencyContactTabSelector = "#sidenav > li:nth-child(3) > a";
        string addButtonID = "btnAddContact";
        string nameInputFieldID = "emgcontacts_name";
        string relationshipInputFieldID = "emgcontacts_relationship";
        string homeTelephoneInputFieldID = "emgcontacts_homePhone";
        string mobileInputFieldID = "emgcontacts_mobilePhone";
        string workTelephoneInputFieldID = "emgcontacts_workPhone";
        string saveButtonID = "btnSaveEContact";

        try
        {
            myInfoTab = driver.FindElement(By.Id(myInfoTabID));

            myInfoTab.Click();

            emergencyContactTab = driver.FindElement(By.CssSelector(emergencyContactTabSelector));

            emergencyContactTab.Click();

            numRows = driver.FindElements(By.XPath(tableRowsXPath)).Count; // Assigning the number of emergency contacts displayed

            addButton = driver.FindElement(By.Id(addButtonID));

            addButton.Click();

            nameInputField = driver.FindElement(By.Id(nameInputFieldID));

            relationshipInputField = driver.FindElement(By.Id(relationshipInputFieldID));

            homeTelephoneInputField = driver.FindElement(By.Id(homeTelephoneInputFieldID));

            mobileInputField = driver.FindElement(By.Id(mobileInputFieldID));

            workTelephoneInputField = driver.FindElement(By.Id(workTelephoneInputFieldID));

            saveButton = driver.FindElement(By.Id(saveButtonID));


            nameInputField.SendKeys("John");
            relationshipInputField.SendKeys("Friend");
            homeTelephoneInputField.SendKeys("(905)-845-2143");
            mobileInputField.SendKeys("(647)-275-2513");
            workTelephoneInputField.SendKeys("(416)-253-2151");

            saveButton.Click();
        }
        catch (Exception e)
        {
            if (e.InnerException != null)
                Console.WriteLine(e.InnerException);
            else
                Console.WriteLine(e);

            driver.Quit();
        }

    }

    private static void PrintTeamAndTestID()
    {
        Console.WriteLine("Team Number: " + team);
        Console.WriteLine("Test ID: " + testID);
    }

    private static Boolean IsEmeregencyContactAdded()
    {
        int postNumRows = 0;

        postNumRows = driver.FindElements(By.XPath(tableRowsXPath)).Count;

        Console.WriteLine(postNumRows);

        if (postNumRows > numRows)
            return true;
        else
            return false;
    }
}
