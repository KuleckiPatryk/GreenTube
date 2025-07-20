Feature: PetStore API tests

    Scenario: Create new pet in pet store
        When User creates new pet in pet store
        Then New pet should be created
        When User gets the created pet
        Then Created pet should match the Get response

    Scenario: Update pet in pet store
        When User updates pet in pet store
        Then Pet should be updated

    Scenario: Delete non existing pet
        When User deletes non existing pet
        Then User should receive NotFound status code

    Scenario: Find pets with expected status
        When User searches for pets with given status
          | Status    |
          | available |
        Then User should get list of pets with expected status
          | Status    |
          | available |