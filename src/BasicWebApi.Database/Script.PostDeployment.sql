/*
Modello di script post-distribuzione							
--------------------------------------------------------------------------------------
 Questo file contiene istruzioni SQL che verranno aggiunte allo script di compilazione		
 Utilizzare la sintassi SQLCMD per includere un file nello script post-distribuzione			
 Esempio:      :r .\myfile.sql								
 Utilizzare la sintassi SQLCMD per fare riferimento a una variabile nello script post-distribuzione		
 Esempio:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO [dbo].[Orders] ([Id], [ORD_DT], [STATUS],[LastModifiedDate],[TotalPrice]) 
VALUES ('52e6e755-88c9-45a2-95be-c425a850d470',GETDATE(), 'New',NULL,10.00)
