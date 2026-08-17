# Turtin's Custom Web Framework

## About this Project: ℹ️
This project is eventually intended to become a between of [Spring Boot](https://spring.io/projects/spring-boot)
built for Java and [NextJS](https://nextjs.org/). Spring Boot is a very manual way of producing a website where every page
and request is manually handled by the user. And NextJS is much easier to use to produce a front end where most back end related
functionality is handled automatically whilst still provide tools to write specifically for the backend. I am a bit of a purist 
(As you might be able to tell from this project) and enjoy manually handling every request, but it is nice to have the easy and quick
workflow provided by NextJS. Currently, these workflows don't exist but I do hope to add compatibility for [React](https://react.dev/)
and eventually my own reactive like framework

## Features ⚡

More to come probably!

| Feature                 | Description                                                                           | Usage                                                     |                        Status                         |
|-------------------------|---------------------------------------------------------------------------------------|-----------------------------------------------------------|:-----------------------------------------------------:|
| Manual request Handling | Allow the user to manually handle any request that they want to easily                | Create a page handler with the attribute and register it. | 🟡 - Support for external hardeners not yet implement |
| Plugin Support          | Allows for plugins to customise functionality and add features                        | Put a plugin in the plugins folder and it loads on start  |                          🔴                           |
| Command System          | Enabled live handling of the server without having to restart providing extra control | Type a command and enter it                               |            🟢 - Not all commands yet added            |
| Request Handling        | The server can receive and produce a response                                         | Create a handler to define the response                   |                          🟢                           |
| Easy Installation       | Server installs itself quickly and easily                                             | Just run the server to install and use                    |                          🟢                           |
| Live logging            | Logs every event to the console live as it happens                                    | It just does it!                                          |                          🟢                           |


## Usage 🚀

Currently, this project is still in early development so this process is subject to change

As no releases exist yet, you will need to compile the code yourself:
1) Clone the repository
2) Build the project using ```dotnet build```
> Note: As this project is still in development, to add you're own pages and handling you will need to edit the
> program directly, in future you will be able to do this externally, also note that no external documentation exists
> so good luck!
3) This should produce an executable and other required files in a build folder,
4) Place these where you want to run the server and run the ```.exe``` file

### Developer Note: 🗒️

This is entirely just a passion project, I always had issues with they way websites are typically made so if I make one my self, then the only person I can blame is my self. I also hate JS so yeah...
I don't typically use C# but it is very similar to Java so I am hoping some of my expirience will be able to cross over.
Currenly this project is in very early works so there is not much to add here, at some point soon I will create a propper ```readme.md``` to better outline my goals and once I am far enough, how to use this. 
Before you judge me, I want to write this with as little libraries as possible
