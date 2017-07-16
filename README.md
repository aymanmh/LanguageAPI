# LanguageAPI
A set of projects demonstraiting the use of various language APIs

The following libraries are used in the bot project to query the followig APIs:
- Bing Translate API
- WordAPI
- Pressmon API
- Wikitionary API

and one project that reads/writes word entries to an Azure Table Storage 

All of the projects can be used independantly, however the proper api keys need to be provided (register the service, and copy the key in the app.config)

###### What is implemented: 
- Calling an API using UniREST .net
- Deserializing JSON requests
- Unit testing with nunit
- Working with Azure Table Storage (conncet/writeg/query)


###### Notes:
- Proper API keys have to be provided inorder for this to work.
- No exception is handled in the libraries, instead are just thrown to be handled by the calling applicaiton.
- All classes are marked as `Seializable` because the Bot app requirs so, otherwise it's not needed.

