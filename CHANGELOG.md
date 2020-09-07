# CG.Core change log
---

## 2020.6

I moved to an azure CI pipeline.

## 2020.5

* I added code to dynamically load missing whitelisted assemblies in the ExtensionMethods extension method.*

* I added code to use reflection to find properties in need of verification, in ValidatableObject.

* I removed the VerifiableBase class and moved that code to ValidatableObjectExtensions.

* I added back the exception extensions.

* I added the directorbase class to the API namespace.

* I changed the ApiBase ctor parameters to an params array of IService objects.

* I added the datetimeextensions class.

* I renamed the SettingsBase class to VerifiableBase.

* I added back the ExtensionMethods method to the appdomain extensions.

* I added the Assembly.ReadXXX extension methods back again.

* I removed ToCamelCase and ToPascalCase extension methods.

* I added a shuffle method to the stringextensions class.

* bug fixes

## 2020.4

* I added ToCamelCase and ToPascalCase to the string extension methods.

* I added the ValidatableObject class.

* I added the RandomEx class.

* I added the SettingBase class.

* I added the FriendlyNameEx extension method back again.

## 2020.3 

* I created a new CI/CD pipeline for the project.

* I merged CG.Core.Primitives with CG.Core

* I merged CG.Core.Abstractions with CG.Core

* I moved the Guard type back to CG.Validations 

* I moved the Retry type back to CG.Utilities

* I added back various test fixtures to the QA project.

* I moved the API types to CG.Apis.

* I reformatted the README.

* I made the services collection protected in the ApiBase class.

* I added back the Task related extensions in CG.Threading.Tasks.

* I added the RetryExtensions type back again.

* I fixed the namespace problem with resources.

## 2020.2

* I fixed various problems with the CI/CD pipeline for the project.
