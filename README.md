### Website: reserved.com 
### Scope: Testing account registration and login functionality 
---
### :ballot_box_with_check: Registration Scenarios
1. Register account without any mandatory fields
    ```
    Scenario: Register account without any mandatory fields
      When User navigates to registration page
        And User clicks create account button on registration page
      Then Each mandatory field should have validation message displayed
        And Account should not be created
    ```
2. Register account with password lesser than 6 characters
    ```    
    Scenario: Register account with password lesser than 6 characters
      When User navigates to registration page
        And User provides valid registration data and weak password
        And User clicks create account button on registration page
      Then Password input should have validation message displayed
        And Account should not be created
    ```
3. Register account with invalid email address
    ```   
    Scenario: Register account with invalid email address
      When User navigates to registration page
        And User provides valid registration data and invalid email address
        And User clicks create account button on registration page
      Then Email input should have validation message displayed
        And Account should not be created
     ```
4. Register account with valid data
    ```  
    Scenario: Register account with valid data
      When User navigates to registration page
        And User provides valid registration data
        And User clicks create account button on registration page
      Then Account should be created
        And User should be redirected to the main page
    ```
5. Register account with already existing email address
    ```       
    Scenario: Register account with already existing email address
      When User navigates to registration page
        And User provides valid registration data with already existing email address
        And User clicks create account button on registration page
      Then Validation message should be displayed that email address already exists in application
        And Account should not be created
    ```
  ### :ballot_box_with_check: Login Scenarios
1. Sign in with valid details
    ```
    Scenario: Sign in with valid details
      When User navigates to login page
        And User provides valid sign in details
        And User clicks sign in button on login page
      Then User should be signed in
        And User should be redirected to the main page
     ```
2. Sign in with invalid details
    ```
   Scenario: Sign in with invalid details
    When User navigates to login page
      And User provides invalid sign in details
      And User clicks sign in button on login page
    Then User should not be signed in
      And Each mandatory field should have validation message displayed
    ```
3. Sign in with empty details
    ```
    Scenario: Sign in with empty details
      When User navigates to login page
        And User clicks sign in button on login page
      Then User should not be signed in
        And Each mandatory field should have validation message displayed
    ```
4. Sign in using google account details
    ```
    Scenario: Sign in using google account details
      When User navigates to login page
        And User clicks sign in with google account button on login page
        And User provides valid google account details
      Then User should be signed in
        And User should be redirected to the main page
    ```
5. Sign in using facebook account details
    ```
    Scenario: Sign in using facebook account details
      When User navigates to login page
        And User clicks sign in with facebook account button on login page
        And User provides valid facebook account details
      Then User should be signed in
        And User should be redirected to the main page
    ```
---
### Automation candidates
- :heavy_check_mark: Register account with valid data - API test + frontend unit test
- :heavy_check_mark: Register account with password lesser than 6 characters - API test + frontend unit test
- :heavy_check_mark: Register account with invalid email address - frontend unit test
- :heavy_check_mark: Register account with valid data - UI test (important for the business to work)
- :heavy_check_mark: Register account with already existing email address - API test + frontend unit test
- :heavy_check_mark: Sign in with valid details - UI test (important for the business to work)
- :heavy_check_mark: Sign in with invalid details - API test
- :heavy_check_mark: Sign in with empty details - API test
- :x: Sign in using google account details - manual test since its a integration with external service, if we can mock it than it can be automated as well
- :x: Sign in using facebook account details - manual test since its a integration with external service, if we can mock it than it can be automated as well
