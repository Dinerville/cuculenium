# Cuculenium
cucumber and selenium combined

## Cuculenium steps options
* Get - get some value of element specified by locator. It can be some attribute value or text
* Set - set some value of element specified by locator. It can be some attribute value or text. Use for inputs
* Click - clicks on some element specified by locator.
* Assert - asserts two values
	
## Cuculenium locators
locators are stored in `locators.yaml` file in following format
```yaml
mainPage:
  numberButton:
    type: xpath
    value: "//button[@value='{0}']"
  addButton:
    type: xpath
    value: "//button[text()='+']"
```

where `mainPage` is a name of the page where locator can be found. 
`numberButton` is the name of locator. Type is normal selenium types.
It can be the following types:  
- ClassName  
- CssSelector  
- Id
- LinkText
- Name
- PartialLinkText
- TagName
- XPath

`value` is the locator  
`{0}` in locator means that this is template locator which can be used in steps with parameters. For example  
```
Click element 'mainPage.numberButton' with parameters '6'
```
For this step the locator will be `//button[@value='6']`, so `{0}` will be replaced with `6`