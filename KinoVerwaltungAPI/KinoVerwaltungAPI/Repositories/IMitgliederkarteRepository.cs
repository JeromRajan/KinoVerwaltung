﻿using KinoVerwaltungAPI.Models;

namespace KinoVerwaltungAPI.Repositories
{
    public interface IMitgliederkarteRepository
    {
        Task<Mitgliederkarte> AddMitgliederkarteAsync();
        Task AufladenAsync(int mitgliederkarteId, decimal betrag);

        Task<Mitgliederstatus> AddMitgliederstatus(Mitgliederstatus mitgliederstatus );
    }
}
