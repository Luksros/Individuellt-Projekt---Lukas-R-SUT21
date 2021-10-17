# Individuellt-Projekt---Lukas-R-SUT21

Programmet är centrerat kring en array med User-objekt, vilka innehåller 5 användare och deras relevanta info (För- och efternamn, 
kontonamn och tillhörande saldo, användarnamn, lösenord).
Vid programstart körs en loginmetod, som låter användaren mata in användarnamn och lösenord upp till tre gånger. 
Vid varje försök söker en for-loop igenom User-arrayen, och om en match på både användarnamn och lösenord görs vid en given iteration, returnerar metoden true, 
vilket tilldelas till boolen loggedIn. Den boolen kommer hålla igång en loop som skriver ut huvudmenyn, dels efter login och sedermera efter varje gång en funktion använts.
Dessutom används en int för att hålla koll på vilket index matchen hittades på.
Menyn funkar genom att en switch körs som väntar på att användaren skriver in en siffra för önskad funktion, 
och därifrån skickar in det "inloggade" användarobjektet i motsvarande metod, med hjälp av ovan nämnda int.
Den första och enklaste metoden är CheckBalance. I stort sett bara en for-loop som går igenom listorna för kontonamn och kontosaldo som finnas lagrade i alla User-objekt,
och skriver ut deras värden. Denna metod kommer återanvändas i alla andra menyval vid något tillfälle (förutom logga ut, förstås). 
Nästa funktion är transferFunds, som används för att överföra saldo från ett konto till ett annat. Först och främst kollar den så att det finns mer än ett konto,
annars fortsätter den inte. Sedan får använda konto att skicka från, sen till, och sen summa. 
AddFunds och WithdrawFunds låter användaren välja ett konto och en summa, och lägger sedan till eller ta bort så mycket från det kontot.
Den längsta metoden med sina ca. 75 kodrader (kommentarer inkluderat) är ManageAccounts. Användaren väljer om denne vill lägga till eller ta bort ett konto.
Lägga till är simpelt: användaren skriver in namnet på det nya kontot, och det läggs till i listan accountNames. En decimal med värdet 0.00 läggs även till
i accounts-listan. Sen är metoden slut. Väljer användaren att ta bort ett konto däremot, så måste vi först kolla så att det finns fler än ett konto tillgängligt, 
annars får användaren inte ta bort ett konto. Annars presenteras användaren med alla tillgängliga konton, och får välja ett att ta bort. Finns det pengar kvar (alltså, 
motsvarande accounts-index är >0), ombeds användaren flytta saldot först. Annars får användaren mata in sitt lösenord, och om lyckat tas kontot bort.
Annars kommer ett felmeddelande. Efter detta är metoden slut. 
Resterande metoder är bara utility-grejer. De skriver ut menyer, spelar en melodi eller ändrar färgen på text. Jag valde att ha dessa saker i egna metoder antingen för
att jag såg att jag hade samma eller liknande stycke med kod på flera ställen i programmet, eller för att snygga till och korta ner metoder och inte tynga ner dem
med massa plottrig text.


UTVÄRDERING
Även fast vi inte har gått igenom objektorientering än så valde jag att centrera mitt program runt objekt, mest av organisatoriska skäl (jag har lite förkunskaper också). 
Då kan jag lätt skapa en "låda"f ör varje användare att lagra all relevant information i, och snabbt och enkelt få tag i det, 
och veta att jag matchar t.ex. rätt användarnamn med rätt lösenord osv.
Jag skulle lika gärna kunna ha använt mig av helt separata listor för varje "informationstyp" (en lista för användarnamn, en för lösen etc). Programmet hade funkat lika bra,
men det kändes enklare i huvudet att skicka in ett objekt i huvudmenyn, och sedan veta att det bara är det objektet med sin tillhörande information som används i 
de olika metoderna.
Det handlar alltså mer om att göra det enklare för mig att skriva programmet än användarvänligheten eller programfunktionalitet. Förmodligen blir det nog även enklare 
för en utomstående programmerare att läsa och förstå programmet. 
Ett annat plus är att det blev lättare att möjliggöra tillägg och borttagning av konton, då jag annars hade fått jobba med listor av listor, eller multidimensionella arrayer. 
Arrayer går ju inte att utöka, men en list-lista hade funkat, men då jag hade liksom haft en låda med massa grenar, som sen ska kopplas ihop med de andra lådorna med namn och 
kontosiffror osv. Jag föredrar objekt, som bara håller koll på SIN relevanta information, och håller det avgränsat från de andra objektens information. 
Hoppas det går att förstå vad jag menar...

En väsentlig förbättring jag hade kunnat göra är att låta alla metoder som körs från menyn ligga inuti User-klassen. Då hade jag inte behövt skicka med index och User-objekt
in i de olika metoderna, utan bara köra de metoderna från objektet självt. Det hade nog lett till lite snyggare och mer koncis kod,  med färre variabler som skyfflas hit och dit. 
Hade jag dessutom haft förkunskaper om arv (vilket jag numera har efter att ha kikat på kommande kurs), så hade jag kunnat använda mig av det för att göra användarnas
konton mer avancerade och flexibla. Istället  för att ha en separat lista för kontonamnen och en för saldona, hade jag kunnat skapa objekt som tillhör klassen user 
och innehåller båda de två sakerna, och förmodligen lätt kunna fixa möjlighet att ha olika valuta på kontona. 

Det stod i instruktionerna att alla konton skulle ha olika antal konton och olika saldon, vilket jag inte har gjort i koden, utan alla börjar med samma. 
Detta var bara ett effektivitetsval, då det är lätt att göra (jag listade förvisso inte ut hur jag fyller i listorna för kontonnamn och -saldo inuti constructorn, 
men jag hade lätt bara kunnat göra manuella ändringar efter att de olika användarna deklarerats), men jag tycker att programmets funktionalitet demonstrerar att jag vet hur
man fixar det, då det går att göra inuti själva programmet. Jag ville bara inte plottra ner main-metoden eller skapa en metod som egentligen bara skulle vara 
onödig utfyllnad i antalet kodrader. 
