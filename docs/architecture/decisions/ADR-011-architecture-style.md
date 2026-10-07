# ADR-011 — Modular Monolith, Ports and Adapters e SOLID

- **ID:** ADR-011
- **Status:** ACCEPTED
- **Data:** 2026-10-07

## Contexto

O produto desktop local exige boundaries testáveis e extensíveis, mas não possui requisito para distribuição operacional.

## Requirements relacionados

RNF-017, RNF-018, RNF-019, RNF-028; CON-003, CON-011.

## Security Requirements relacionados

SEC-007, SEC-032 a SEC-034.

## Research Evidence

RES-008, RES-010, RES-015, RES-017 e Technical Research Report.

## Opções consideradas

Monólito sem módulos; Modular Monolith; microservices/distributed messaging.

## Decisão

Adotar Modular Monolith no Assistant Core, Ports and Adapters nos boundaries e SOLID de forma pragmática. Separar processo do OBS é isolamento, não microservice. Rejeitar infraestrutura distribuída no V1.

## Justificativa

Entrega a menor complexidade suficiente, com coesão, dependency inversion e extensibilidade real de providers.

## Trade-offs

Disciplina de módulos e architecture tests são necessárias; operação e deployment permanecem simples.

## Consequências positivas

SRP, OCP, LSP, ISP e DIP verificáveis; mudanças de adapter localizadas.

## Consequências negativas

Módulos compartilham processo e precisam evitar acoplamento acidental.

## Security Impact

Ports tornam least privilege, validação e policy gates explícitos.

## OBS Impact

O Core modular permanece fora do OBS; plugin não participa do monólito de negócio.

## Implementation Impact

Dependency rules e composition root únicos; sem frameworks arquiteturais obrigatórios.

## Validation Required

Architecture tests, provider contract tests e revisão de dependências.

## Supersedes

Nenhum.

## Superseded By

Nenhum.
