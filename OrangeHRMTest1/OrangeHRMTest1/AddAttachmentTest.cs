using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System;
using System.IO;

class AddAttachmentTest
{
    static IWebDriver driver = new ChromeDriver();

    static string testID = "TC_IM_01";
    static string team = "05";

    static string tableRowsXPath = "//table[@id='tblAttachments']/tbody/tr";
    static int numRows = 0; // Will hold the number of current attachments


    // Setting the absolute filepath for the file to be uploaded
    static string AbsoluteFilePath = Path.GetFullPath("..\\..\\files\\ma_kore.txt");

    static void Main()
    {
        // Printing team # and test ID
        PrintTeamAndTestID();

        // Login 
        Login();

        // Navigating to the emergency contact tab and adding 1 contact
        AddAttachment();

        if (IsFileAttached())
            Console.WriteLine("File uploaded successfully!");
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

    private static void AddAttachment()
    {
        IWebElement myInfoTab;
        IWebElement emergencyContactTab;
        IWebElement addButton;
        IWebElement uploadButton;
        IWebElement fileInputField;

        string myInfoTabID = "menu_pim_viewMyDetails";
        string emergencyContactTabSelector = "#sidenav > li:nth-child(3) > a";
        string addButtonID = "btnAddAttachment";
        string uploadButtonID = "btnSaveAttachment";
        string fileInputFieldID = "ufile";

        try
        {

            myInfoTab = driver.FindElement(By.Id(myInfoTabID));

            // Going to -> My info Tab
            myInfoTab.Click();


            emergencyContactTab = driver.FindElement(By.CssSelector(emergencyContactTabSelector));

            // Going to -> Emergency Contact Tab
            emergencyContactTab.Click();

            // Counting how many attachments are present prior to attaching the file
            numRows = driver.FindElements(By.XPath(tableRowsXPath)).Count;


            addButton = driver.FindElement(By.Id(addButtonID));

            // Clicking ADD
            addButton.Click();

            fileInputField = driver.FindElement(By.Id(fileInputFieldID));

            // Uploading the file
            fileInputField.SendKeys(AbsoluteFilePath);

            uploadButton = driver.FindElement(By.Id(uploadButtonID));

            // Saving the uploaded file
            uploadButton.Click();
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

    private static Boolean IsFileAttached()
    {
        int postNumRows = 0;

        postNumRows = driver.FindElements(By.XPath(tableRowsXPath)).Count;

        if (postNumRows > numRows)
            return true;
        else
            return false;
    }

    private static void PrintTeamAndTestID()
    {
        Console.WriteLine("Team Number: " + team);
        Console.WriteLine("Test ID: " + testID);
    }
}
