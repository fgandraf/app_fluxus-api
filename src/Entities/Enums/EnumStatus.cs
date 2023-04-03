﻿using System.Text.Json.Serialization;

namespace FluxusApi.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumStatus
    {
        RECEBIDA = 1,
        PENDENTE = 2,
        VISTORIADA = 3,
        CONCLUIDA = 4
    }
}