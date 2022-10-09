/*
 Modello di script pre-distribuzione							
--------------------------------------------------------------------------------------
 Questo file contiene istruzioni SQL che verranno eseguite prima dello script di compilazione	
 Utilizzare la sintassi SQLCMD per includere un file nello script pre-distribuzione			
 Esempio:      :r .\myfile.sql								
 Utilizzare la sintassi SQLCMD per fare riferimento a una variabile nello script pre-distribuzione		
 Esempio:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
