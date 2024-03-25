<h1 align="center"> League Project </h1>

## Install
1. Clone the repository
```
$ git clone  https://github.com/MarcosOlvr/LigaDasLendas.git
```

2. Access the folder
```
$ cd LigaDasLendas
```

3. Install .NET libraries
```
$ dotnet add package RiotSharp
```
```
$ dotnet add package Camille.RiotGames
```
```
$ dotnet add package Microsoft.AspNet.Cors
```

4. Install VueJs dependencies
```
$ npm i
```

# API EndPoints
You'll need a key to access the RIOT API. Go to https://developer.riotgames.com/apis and make your request.

Then create a file like this:
![example](images/image.png)

## Summoner 
* GET *api/summoner/summonerName-tagLine*
* GET *api/masteries/summonerName-tagLine*
* GET *api/icon/summonerName-tagLine*
* GET *api/league/summonerName-tagLine*
* GET *api/runes*
* GET *api/runes/runeName*
* GET *api/spells*

## Champions
* GET *api/champ/{skip}/{take}*
* GET *api/champ/champId*
* GET *api/champ/champName*

## Matches
* GET *match/latest/{riotId}-{tagLine}/*

<br>

# FrontEnd Pages 

### Home
![index page](images/image-1.png)
![alt text](images/image-11.png)

### Champions
![alt text](images/image-2.png)

![alt text](images/image-3.png)

![alt text](images/image-4.png)

### Champion (Lucian)
![alt text](images/image-6.png)
![alt text](images/image-7.png)
![alt text](images/image-8.png)

### Runes 
![alt text](images/image-9.png)

### Summoner Spells
![alt text](images/image-10.png)