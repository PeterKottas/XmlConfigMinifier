## Xml Config Minifier

Have you ever seen this error in your asp.net website? 

"Cannot read configuration file because it exceeds the maximum file size" 

That's because maximum allowed size for web.config is 250kb. 
You can easily overshoot that especially if you are using lof of libraries or a CMS like sitecore. 
This tool helps you fix it by removing comments, white spaces, and providing custom indentation. 

## Code Example

The example config included in the library goes from 1,689 bytes to 259 bytes. 

Try it yourself using these parameters : 

*-f="ExampleConfig\\Web.config"*

Check *-help* for more info about the parameters. 

## Motivation

Include it in your build process as a post build step and have some light weight config for a change! 

## Contributors

Feel free to contribute, open issues or use this tool. I'll be more than happy to assis you in doing so. For contributing, please create a new branch out of master and then when ready, stare a new pull request.





