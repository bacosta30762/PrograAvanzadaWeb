using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPrograAvanzadaWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedAeropuertos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Aeropuertos (IATA, Nombre, Ciudad, Pais)
            VALUES
                ('ATL', 'Hartsfield-Jackson Atlanta International Airport', 'Atlanta', 'Estados Unidos'),
                ('PEK', 'Beijing Capital International Airport', 'Beijing', 'China'),
                ('DXB', 'Dubai International Airport', 'Dubái', 'Emiratos Árabes Unidos'),
                ('LAX', 'Los Angeles International Airport', 'Los Ángeles', 'Estados Unidos'),
                ('HND', 'Tokyo Haneda Airport', 'Tokio', 'Japón'),
                ('ORD', 'Chicago O''Hare International Airport', 'Chicago', 'Estados Unidos'),
                ('LHR', 'Heathrow Airport', 'Londres', 'Reino Unido'),
                ('CDG', 'Charles de Gaulle Airport', 'París', 'Francia'),
                ('DFW', 'Dallas/Fort Worth International Airport', 'Dallas/Fort Worth', 'Estados Unidos'),
                ('FRA', 'Frankfurt am Main Airport', 'Fráncfort', 'Alemania'),
                ('IST', 'Istanbul Airport', 'Estambul', 'Turquía'),
                ('CAN', 'Guangzhou Baiyun International Airport', 'Cantón', 'China'),
                ('JFK', 'John F. Kennedy International Airport', 'Nueva York', 'Estados Unidos'),
                ('SIN', 'Singapore Changi Airport', 'Singapur', 'Singapur'),
                ('AMS', 'Amsterdam Airport Schiphol', 'Ámsterdam', 'Países Bajos'),
                ('PVG', 'Shanghai Pudong International Airport', 'Shanghái', 'China'),
                ('DEL', 'Indira Gandhi International Airport', 'Delhi', 'India'),
                ('ICN', 'Incheon International Airport', 'Incheon', 'Corea del Sur'),
                ('GRU', 'São Paulo/Guarulhos–Governador André Franco Montoro International Airport', 'São Paulo', 'Brasil'),
                ('BKK', 'Suvarnabhumi Airport', 'Bangkok', 'Tailandia'),
                ('SFO', 'San Francisco International Airport', 'San Francisco', 'Estados Unidos'),
                ('LAS', 'Harry Reid International Airport', 'Las Vegas', 'Estados Unidos'),
                ('SEA', 'Seattle-Tacoma International Airport', 'Seattle', 'Estados Unidos'),
                ('MUC', 'Munich Airport', 'Múnich', 'Alemania'),
                ('MCO', 'Orlando International Airport', 'Orlando', 'Estados Unidos'),
                ('EWR', 'Newark Liberty International Airport', 'Newark', 'Estados Unidos'),
                ('YYZ', 'Toronto Pearson International Airport', 'Toronto', 'Canadá'),
                ('SYD', 'Sydney Kingsford Smith Airport', 'Sídney', 'Australia'),
                ('MIA', 'Miami International Airport', 'Miami', 'Estados Unidos'),
                ('BCN', 'Barcelona–El Prat Airport', 'Barcelona', 'España'),
                ('KUL', 'Kuala Lumpur International Airport', 'Kuala Lumpur', 'Malasia'),
                ('HKG', 'Hong Kong International Airport', 'Hong Kong', 'China'),
                ('MAD', 'Adolfo Suárez Madrid–Barajas Airport', 'Madrid', 'España'),
                ('DEN', 'Denver International Airport', 'Denver', 'Estados Unidos'),
                ('BOM', 'Chhatrapati Shivaji Maharaj International Airport', 'Mumbai', 'India'),
                ('DOH', 'Hamad International Airport', 'Doha', 'Catar'),
                ('PHX', 'Phoenix Sky Harbor International Airport', 'Phoenix', 'Estados Unidos'),
                ('IAH', 'George Bush Intercontinental Airport', 'Houston', 'Estados Unidos'),
                ('CLT', 'Charlotte Douglas International Airport', 'Charlotte', 'Estados Unidos'),
                ('MEL', 'Melbourne Airport', 'Melbourne', 'Australia'),
                ('FCO', 'Leonardo da Vinci–Fiumicino Airport', 'Roma', 'Italia'),
                ('SVO', 'Sheremetyevo International Airport', 'Moscú', 'Rusia'),
                ('LGA', 'LaGuardia Airport', 'Nueva York', 'Estados Unidos'),
                ('CPH', 'Copenhagen Airport', 'Copenhague', 'Dinamarca'),
                ('OSL', 'Oslo Gardermoen Airport', 'Oslo', 'Noruega'),
                ('ARN', 'Stockholm Arlanda Airport', 'Estocolmo', 'Suecia'),
                ('SJO', 'Juan Santamaría International Airport', 'San José', 'Costa Rica');
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Aeropuertos WHERE IATA IN (
                'ATL', 'PEK', 'DXB', 'LAX', 'HND', 'ORD', 'LHR', 'CDG', 'DFW', 'FRA', 'IST', 'CAN', 'JFK', 'SIN', 'AMS', 
                'PVG', 'DEL', 'ICN', 'GRU', 'BKK', 'SFO', 'LAS', 'SEA', 'MUC', 'MCO', 'EWR', 'YYZ', 'SYD', 'MIA', 'BCN', 
                'KUL', 'HKG', 'MAD', 'DEN', 'BOM', 'DOH', 'PHX', 'IAH', 'CLT', 'MEL', 'FCO', 'SVO', 'LGA', 'CPH', 'OSL', 
                'ARN', 'SJO'
            );");
        }
    }
}
