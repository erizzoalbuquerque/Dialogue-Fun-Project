using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using Yarn.Markup;
using Yarn.Unity;

public class PunctuationPauseHandler : ActionMarkupHandler
{
    [Serializable]
    public struct PausePreset
    {
        [Tooltip("Todos os caracteres deste grupo usam a mesma pausa. Ex: \".!?…\"")]
        public string characters;
        [Min(0f)] public float seconds;
    }

    [SerializeField]
    private List<PausePreset> pauses = new()
    {
        new PausePreset { characters = ",;:",  seconds = 0.15f },
        new PausePreset { characters = ".!?…", seconds = 1f },
    };

    [Tooltip("Em sequências como \"...\" ou \"?!\", pausa só no último caractere da sequência repetida.")]
    [SerializeField]
    private bool pauseOnlyAtEndOfRun = true;

    private readonly Dictionary<char, float> lookup = new();

    private void Awake() => RebuildLookup();
    private void OnValidate() => RebuildLookup();

    private void RebuildLookup()
    {
        lookup.Clear();
        foreach (var preset in pauses)
        {
            if (string.IsNullOrEmpty(preset.characters)) continue;
            foreach (char c in preset.characters)
                lookup[c] = preset.seconds; // o último grupo da lista vence em caso de repetição
        }
    }

    public override void OnPrepareForLine(MarkupParseResult line, TMP_Text text) { }
    public override void OnLineDisplayBegin(MarkupParseResult line, TMP_Text text) { }
    public override void OnLineDisplayComplete() { }
    public override void OnLineWillDismiss() { }

    public override async YarnTask OnCharacterWillAppear(
        int currentCharacterIndex, MarkupParseResult line, CancellationToken cancellationToken)
    {
        // Este método roda ANTES do caractere atual aparecer.
        // Então "pausa depois do caractere X" = olhar o caractere anterior.
        if (currentCharacterIndex <= 0 || cancellationToken.IsCancellationRequested) return;

        string visible = line.Text; // texto já sem as tags de markup
        if (currentCharacterIndex >= visible.Length) return;

        char previous = visible[currentCharacterIndex - 1];
        if (!lookup.TryGetValue(previous, out float seconds) || seconds <= 0f) return;

        if (pauseOnlyAtEndOfRun && visible[currentCharacterIndex] == previous) return;

        try
        {
            await YarnTask.Delay(TimeSpan.FromSeconds(seconds), cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // Jogador pulou a linha: engolimos o cancelamento e o typewriter termina normalmente.
        }
    }
}