using System;
using System.Collections.Generic;
using System.Linq;
using SimpleScriptLanguageCompiler.Common;
using SimpleScriptLanguageCompiler.Tokens;
using Stateless;

namespace SimpleScriptLanguageCompiler.LexicalAnalysis {
    public class TokenProcessor {
        private TokenState State = TokenState.InitialState;
        private StateMachine<TokenState, char> StateMachine { get; }

        public TokenProcessor() {
            StateMachine = new(() => State, s => State = s);
            ConfigureStateMachine();
        }

        public (TokenIdentifier token, int? nextChar) ReadToken(string content, int initialChar = 0) {
            if (content.Length < initialChar) throw new Exception($"{nameof(ReadToken)} should receive at least one readable char");

            var buffer = "";
            var lastCharRead = initialChar;
            for (; lastCharRead <= content.Length; lastCharRead++) {
                try {
                    StateMachine.Fire(content[lastCharRead]);
                } catch (Exception e) {
                    break;
                }
                buffer = $"{buffer}{content[lastCharRead]}";
            }
            var tokenType = StateMachine.State switch {
                TokenState.Identifier => KeywordFinder.Find(buffer),
                _ => TokenEnum.UNKNOWN
            };
            var token = new TokenIdentifier {
                Token = tokenType
            };

            ResetState();
            return (token, (lastCharRead == content.Length) ? null : lastCharRead);
        }

        private void ResetState() =>
            State = TokenState.InitialState;

        private void ConfigureStateMachine() {
            StateMachine.Configure(TokenState.InitialState)
                .PermitForAll(Enumerable.Range('a', 26).Select(x => (char)x), TokenState.Identifier);

            StateMachine.Configure(TokenState.Identifier)
                .IgnoreForAll(Enumerable.Range('a', 26).Select(x => (char)x));
        }
    }

    enum TokenState {
        InitialState,
        Identifier
    }

    // TODO: Move to common
    static class StateMachineExtensions {
        public static StateMachine<TState, T>.StateConfiguration PermitForAll<TState, T>(this StateMachine<TState, T>.StateConfiguration config,
            IEnumerable<T> objects,
            TState destination) {
            objects.ForEach(obj => {
                config.Permit(obj, destination);
            });
            return config;
        }

        public static StateMachine<TState, T>.StateConfiguration IgnoreForAll<TState, T>(this StateMachine<TState, T>.StateConfiguration config,
            IEnumerable<T> objects) {
            objects.ForEach(obj => {
                config.Ignore(obj);
            });
            return config;
        }
    }
}