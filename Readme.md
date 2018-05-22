Anagram Finder
==============


To Run:

Download and install dotnet core (runs on mac/linux! No windows required) - https://www.microsoft.com/net/learn/get-started/

Check out the project, cd to the root directory and do the following: 

``` 
$ dotnet restore
$ dotnet run
```

That should be it! API will start on port 5000 and will default to Swagger docs describing the API. 

## Directory Structure

- Controllers: controllers (duh!) 
- Extensions: extension methods for strings (only one method now, but these tend to grow)
- Models: DTOs for models coming in/out of the API
- Repositories: Data access classes. Used for interfacing with the data, in this case a text file and the Corpus

Program.cs starts the project and Startup.cs is for configuring the API. 

## Implementation Details

Internal data structure is a dictionary with sorted words as the key and the the original words as the values. e.g.,

```
// the following words would create this in the dictionary: read, dare, dear
dict['ader'] => ["read", "dare", "dear"]
```

This way, to test for anagrams, it's a simple matter of sorting the word and then doing a quick lookup. All anagrams are stored with each other. 

The API design is probably a little overkill for the size of the project, but is meant to demonstrate how a larger project might work. This includes:

* All repositories apis are enforced by an interface and then instantiated through dependency injection. The interfaces are silly with only one repository, but obviously become very useful as a project grows. 
* DTOs or models are defined in a separate namespace
* Each namespace is built in a way so that in can easily be built into separate libraries/modules. This way other projects, say background jobs, can easily pull in model and repository code as needed.  

In previous projects, models, repositories and extensions have all been separate libraries allowing for more flexible modularization. I didn't do that here, but the intent is the same with the folder structure. 

## Considerations

I considered a tree at first, but given the simplicity of the dictionary solution and the fact that no default/generic tree data structure exists in .net, I went with the dictionary. Given more time, a tree/trie would be fun to build. 

## TODO:

- Endpoint to return all anagram groups of size >= *x*
