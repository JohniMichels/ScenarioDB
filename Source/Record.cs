using System.Collections.Generic;
using System;

namespace ScenarioDB;
public class Record
{
    public string Path { get; init; } = "";
    public string Id { get; init; } = "";

    public IDictionary<string, dynamic> Fields { get; init; } = 
        new Dictionary<string, dynamic>();

}