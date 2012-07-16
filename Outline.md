Preflight Checklist
===
* Ignore packages folder

* Setup Demo Branch
* Configure Build Server to monitor Demo Branch
* Clear NuGet package cache
* Warm up Visual Studio



Resources
===
* LLewellyn's Video: [Using ApprovalTests in .Net 20 Asp.Mvc Views](http://www.youtube.com/watch?v=SttlPzwJw3U)
* Chris's Video: [Approval Tests ILoader Example](http://www.youtube.com/watch?v=5gIeJ6z82Pk)

Introduction
===
* Unit testing MVC Views not easy at first.
* ApprovalTests make it possible.
* It's easy to repeat once you know how.
* Plan
	* File-> New Project
	* Setup Basic Tests
	* Setup Data-Dependent Tests
	* Make sure everything works in:
		* Visual Studio
		* NCrunch
		* CruiseControl.NET
	* Make a few mistakes along the way, resolve them.
* [GitHub Repository](https://github.com/jamesrcounts/aspConf-demo)

Project Setup
===

MVC3 Intranet Site
---

* Works with any thing a webserver can render.
* Must have test project
	* ApprovalTests works with many popular frameworks
	* I use MSTest
* Enable package restore

Choose Web Server
---

* ApprovalTests can approve any accessible web page.
* You need to make your code acessible to ApprovalTests
	* Cassini (easiest to get started)
	* IIS
	* IIS Express
	* CassiniDev
* Testing Configuration Must Allow Anonymous Requests

Run the Default Tests
---

* 2 controller tests included in intranet template
	* Run them in Visual Studio 
	* Configure and Run NCrunch
	* Commit, Push and Force Build
* Closer look at controller tests
	* MVC Makes controller and Model Tests Easy
	* ViewResult != View

View Tests
---

* Above all else, users care about the view
* We write the view, we should test the view
	* Protection from Regression
	* Design with feedback
	* Executable specification
* If we can make View tests easy, why not?

###Testing Views With ApprovalTests
* `Install-Package ApprovalTests -projectname CarDealership.Tests`
* Create `Views` folder to hold tests
* Add new `Basic Unit Test` called `HomeViewsTest`
* Import namespaces
	* `using ApprovalTests.Asp;`
	* `using ApprovalTests.Asp.Mvc;`
	* `using ApprovalTests.Reporters;`
* Decorate class with `UseReporterAttibute`
	* `FileLauncherReporter` allows you to review the rendered result.
	* `TortiseDiffReporter` quickly pinpoints small differences in HTML
		* ApprovalTests supports many diff tools out of the box
		* Adding your own is easy.  
		* I've written two, `CodeCompareDiffReporter` and `VisualStudioReporter` (alpha)
	* ProTip: `UseReporterAttibute` can have `assembly:` scope.
	* Create `TestMethod` for each `View` to test.
		* `TestIndexView`
		* `TestAboutView`
	* ApprovalTest Process in English
		* `// Make test target available on web server`
		* `// Supply route to MVC Action`
		* `// Test action result (RED)` 
		* `// Review result`
		* `// Approve Result (if appropriate)`
		* `// Run Again (GREEN)`
		* `// Refactor`
	* ApprovalTest Process in Action
		* `CTRL+F5` to start the site.
		* `Func<ActionResult> actionDelegate = new HomeController().Index;`
		* `MvcApprovals.VerifyMvcPage(actionDelegate);`
			* Failures:
				* `MvcPort` not set, go back to "supply the route" and set `PortFactory.MvcPort`
				* Approval Failure, expected for first run of any ApprovalTest. Run in `VS`
		* Review Failing Result
			* Notice that the browser shows us a local file.
			* `TortoiseMerge` shows us the `HTML`
		* Assume this is what we want, and approve.
			* Golden side will now be used for all future comparisons.
		* Run Again
			* Passes in VS
			* Force NCrunch to run, passes
		* Refactor, not necessary for demo
			* Use failing ApprovalTest to design with feedback.
			* use passing ApprovalTest for protection from regression.
			* Same as any other TDD.
		* Commit/Push
			* Ignore .received.* files.
	* Refactor before going to second test.
		* Inline delegate.
		* Delete Comments
		* Move `MvcPort` to base class.
		* MvcApproval is now one line of code.
	* Write `TestAboutView`
		* Review Result
		* Approve
		* Run Again
		* Commit and Push
* Tackling the Build Server
	* Test works fine in localhost
		* Works in NCrunch
		* Works in Visual Studio
	* Fails on build server
		* First precondition for MvcApproval is not met 
		* Test server is not visible from the build sever
	* Build server must be able to make target available when needed
		* Open source Web Server CassiniDev allows self-hosting in test.
		* `Install-Package CassiniDev -ProjectName CarDealership.Tests`
	* Update MvcViewTest base class.
		* Import namespace `using CassiniDev;`
		* Add `[TestClass]` attribute
		* Inherit `CassiniDevServer`
		* Add Empty `Setup` and `Teardown` methods.
			* Decorate `Teardown` with `[TestCleanup]`
			* Decorate 'Setup' with `[TestInitialize]`
		* Implement `Teardown` *first* using this line
			* `this.StopServer()`
		* Implement `Setup` with this line.
			* `this.StartServer(@"..\..\..\CarDealership", PortFactory.MvcPort, "/", "localhost");`
	* Including CassiniDev breaks the NCrunch and VisualStudio tests
		* Error indicates that our MVC port is in use.
		* CassiniDev must use different port than Cassini
		* I usually give CassiniDev MvcPort++
	* Test fails again.  
		* Port number is part of the approved HTML.
		* We can approve this.
		* We also see that our result page is not styled.
		* CSS is not part of the approved file.  
			* Persistent web server can serve the file to us during approval
			* CassiniDev shuts down before Chrome requests CSS.
		* Using self-hosting creates a divide 
			* Cassini/IIS Express better when designing with feedback.
			* CassiniDev good for automation and regression protection.
	* After approval, test passes in Visual Studio, fails in NCrunch
		* NCrunch has 500 error
		* In NCrunch Build environment, path to `CarDealership` is different.
		* Test relies on bad assumption about the Build environment.
		* NCrunch revals this assumption.
	* Use `ApprovalUtilities` to dynamically locate the MvcApplication.
		* Want `Setup` to look like this:
			* `this.StartServer(MvcApplication.Directory, PortFactory.MvcPort, "/", "localhost");`
		* Use Refactoring tool to create `Directory`
			* Delete setter.
			* Implement getter.
				* `return PathUtilities.GetDirectoryForCaller();`
		* `Install-Package ApprovalTests -ProjectName CarDealership`
		* You need `ApprovalUtilities` but you also get `ApprovalTests`.
			* You can delete `ApprovalTests` if it bothers you.
		* Import namespace `ApprovalUtilities.Utilities`
	* NCrunch notices the new implementation 
		* All tests pass in NCrunch.
	* Run in Visual Studio
		* May see 2 failures.
		* Results reveal port conflict. 
		* Race condition NCrunch vs Visual Studio.
		* Run again.
	* Commit and Push
	* Failure because user name is different on the server.
		* Update Views to remove user name
		* Approve.
* Working with Data
	* Tests work in three environments
	* Our tests views are static
	* Real views are dynamic
	* Lets add some data
	* Create two classes
		* `CarLot` - Repository
		* `Car` - Entity
	* `CarLot` always throws an exception.
	* Create an Action that displays a list of cars.
		* `// Get the data`
		* `// Show the data`
	* To Get Data, we need a repository
		* Implement default constructor
		* Implement injectable constructor
	* Implement and Build
	* Add strongly typed list view.
	* Test it
		* Request fails with 500 error
		* Switch to desktop Cassini 
		* Problem is unavailable repository.
	* Can't mock it, server has its own copy of `HomeController`.
	* We must create a test seam to bypass.
		* Cars action does two things
			* Get the data
			* Show the data
		* To test, we need to reimplement Get The Data so that it doesn't use the repository
		* Separate Get the Data from Show The Data by using Extract Method to create a new private method
		* `private ViewResult Cars(IEnumerable<Car> theData){return View(theData);}`
		* This is the part that doesn't change.
		* Before implementing the part that does change, take steps to isolate
			* Make HomeController partial
			* Create file HomeControllerTest
			* Add other half of the partial to the HomeControllerTest file.
		* Implement 	`TestCars`
			* `public ViewResult TestCars(){...}`
			* This method will new up test data
			* Pass it to private seam.
		* Update test to call TestCars
	* Test fails now because MVC looks for a view called `TestCars`
		* Update seam to call `View` with explicit name.
		* `return View("Cars", theData");`
	* Now we get feedback from ApprovalTests.  
		* We can see that removing the login name has screweed up CSS
		* We don't care about that right now.
		* Approve
	* Retest passes.
	* Cleanup Magic String
		* Call with Explicit ViewName introduces magic string
		* Magic strings resist refactoring
		* Remove with Approval Utilities
			* `using ApprovalUtilities.Asp.Mvc;`
			* `return View(theData).Explicit();`
		* Protect From Inlining
			* Turn off optimization
			* Or Add Attribute
				* `[MethodImpl(MethodImplOptions.NoInlining)]`
				* `CTRL-.` to add `using System.Runtime.CompilerServices;`
	* Exclude Test Code from Release Deployments
		* `HomeControllerTest` line 1: `#if DEBUG`
		* `HomeControllerTest` line last++: `#endif`
		* Switch to Release mode.
		* Creates compilation error in Test project. 
		* Exclude `TestCars` as well.
	* Finally Reconfigure For Automated Tests
		* Protection from Regression When Not Desigining
		* Will let the tests pass on the build server.
		* Uncomment `PortFactory.MvcPort++;`
		* Uncomment `this.StartServer(...);`
		* Re-run tests.
		* Approve failing tests.
		* Watch out for race condition.
		* ReRun NCrunch
		* Commit and Push


		

