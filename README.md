# GymHUB
GymHUB är en hemsida för bokning av träningspass. Tanken med den är att det ska vara enkelt att gå in, se vilka pass som finns, boka en plats och sedan hålla koll på sina bokningar utan att behöva hoppa runt mellan olika delar. Det finns också en admin-del i systemet, så att pass, instruktörer, salar och bokningar kan hanteras på ett smidigt sätt. Kort sagt är syftet med hemsidan att samla allt kring träningsbokning på ett och samma ställe och göra det tydligt både för den som bokar och den som administrerar.

## Funktioner

### För användare
- Registrera konto och logga in
- Se kommande träningspass
- Öppna detaljvy för ett pass
- Boka pass om plats finns
- Se egna bokningar
- Avboka bokningar
- Aktivera tvåstegsverifiering

### För administratör
- Hantera träningspass
- Hantera salar
- Hantera instruktörer
- Se bokningar i systemet

## Tekniker
- ASP.NET Core MVC
- C#
- Entity Framework Core
- ASP.NET Identity
- SQLite
- Razor Views
- Bootstrap

## Datamodell

Applikationen bygger på följande centrala modeller:
- `User`
- `Booking`
- `ClassSession`
- `Room`
- `Instructor`
- `InstructorRoom`

## Av Haris Sabanovic
