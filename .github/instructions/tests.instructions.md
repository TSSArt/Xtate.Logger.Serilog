---
applyTo: "test/**/*.cs"
---

# Test source instructions

## Test style

- Use MSTest attributes and the assertion style already used by nearby tests.
- Keep tests focused, deterministic, independent, and safe under parallel execution.
- Capture events with an in-memory or controlled Serilog sink.
- Do not require Seq, another network service, console inspection, or timing.

## Coverage

- Assert the mapped level, rendered/message template, exception, source context, and structured properties as applicable.
- Cover destructuring of Xtate data-model values when conversion behavior changes.
- Verify writer/logger disposal when ownership or lifetime behavior changes.
- Keep Xtate.IoC setup minimal and representative of public registration usage.

## Verification

- Run the narrowest matching test on one modern framework first.
- Run broader solution tests and legacy targets when shared or compatibility-sensitive behavior changes.
