# Roadmap

TunnelX is free and open-source. This roadmap is not a promise of delivery, but a public place to track useful future improvements.

## Planned Ideas

### System preflight and guided repair

Add an in-app system check before connecting that can detect common local problems and guide the user clearly.

Candidate checks:

- Administrator privileges
- Runtime mode: standalone/self-contained vs framework-dependent developer build
- Xray and sing-box availability
- WinDivert and Wintun native component availability
- extraction folder write permissions
- route and packet interception readiness
- deeper release asset verification for future automatic update flows

Potential actions:

- re-extract bundled components when possible
- show a clear manual fix guide when automatic repair is not possible
- ask before downloading any external file
- download only from official TunnelX GitHub Releases
- verify downloaded files before use
- avoid silent driver or system-level changes

For public releases, TunnelX should continue to prefer self-contained standalone EXE builds so end users do not need to install the .NET Runtime separately.

### Profile management

- import and export connection profiles
- clone profiles with clearer naming
- per-profile health and last-test status
- presets for common workflows such as browser-only, messaging, development, and full VPN

### Split tunnel rule clarity

- explicit rule priority between apps, include destinations, and exclude destinations
- wildcard and domain-suffix previews
- conflict detection when the same destination is both included and excluded
- show which rule caused a destination to be routed or bypassed

### Health dashboard

- user-facing states: safe, protected, needs attention, and broken
- short explanation for the latest important network event
- suggested action next to DNS, IPv6, route, and leak status

### Kill switch

- optional per-app kill switch when the tunnel core or TUN bridge drops
- keep selected apps blocked until the tunnel is restored or the user disconnects

### Traffic accounting clarity

- keep tunnel, direct, per-app, DNS, and history counters aligned to one accounting model
- expose whether counters are interface-based, rewritten-flow-based, or app-attributed

### Installer and prerequisite checks

- optional installer/bootstrapper for shortcuts, uninstall support, prerequisite checks, and future update flow
