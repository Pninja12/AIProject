# Projeto 1
## Inteligência Artificial

**Ivan Emidio a22301234**  
**Paulo Silva a22206205**

---

## Contribuições

### Ivan Emidio:
- Script `SpecIA`: Essencial da FSM (65% do script).
- Script `Explosion`: (90% do script).
- Script `ExplosionManager`: Tudo.
- Script `FireSpread`: (90% do script).
- Adicionei Prefabs `Fire`, `Capsule` (player).
- Criei botão para dar Spawn de mais agentes, e o respetivo método `SpawnMoreAgents`.

### Paulo Silva:
- Script `SpecIA`: Essencial da FSM (35% do script)
- Script `Explosion`: (10% do script)..
- Script `FireSpread`: (10% do script).
- Criação do cenário.
- Criação de texto UI
- Script `GameBrainer`: Tudo
- Criação do NavMesh e do Agent
- Organização e criação dos terrenos para NavMesh

---

## Introdução

O projeto desenvolvido consiste numa simulação de vários agentes durante um evento de concertos utilizando Inteligência Artificial no Unity.  
O objetivo principal foi criar agentes que, de forma autônoma, reagissem a diferentes necessidades (fome e cansaço) e a eventos inesperados (explosões).

Para resolver o problema, foi implementado um sistema de inteligência baseado numa **Máquina de Estados Finita (FSM)**, que permite aos agentes mudarem de estado conforme as suas necessidades.

Tínhamos como ideia principal também adicionar um sistema de propagação de pânico onde agentes próximos de outros em fuga reagissem da mesma maneira, mas não conseguimos implementar a tempo essa reação.  
No entanto, ficou implementada a reação de pânico dos agentes que se encontram mais próximos da explosão, fugindo para o lado contrário ao centro da explosão.

Os resultados alcançados confirmaram o sucesso da implementação da FSM nos agentes, comportando-se de forma realista e adaptativa de acordo com as suas necessidades, além da demonstração de comportamentos emergentes durante situações de pânico.

---

## Metodologia

A simulação foi implementada em **3D** utilizando a engine **Unity**.

O cenário principal consiste numa área de concertos dividida em:
- Três palcos (concertos),
- Duas zonas verdes (zonas de descanso),
- Duas zonas de alimentação.

Adicionalmente, foram criadas zonas de entrada e saída para permitir a evacuação dos agentes em caso de pânico (embora não estejam operacionais).

O movimento dos agentes foi implementado de forma **dinâmica**, utilizando a **NavMesh** do Unity, que calcula e percorre caminhos automaticamente.

Cada agente possui:
- Velocidade base,
- Velocidade em pânico,
- Velocidade reduzida.

Foi utilizada uma **Máquina de Estados Finita (FSM)** para controlar os comportamentos dos agentes.  
Cada agente pode estar num dos seguintes estados:
- `Idle` (inicial),
- `WatchingConcert` (assistir a concertos),
- `GoingToGreenZone` (descansar),
- `GoingToFoodZone` (comer).

A mudança de estado depende dos seguintes fatores:
- **Energia** (se abaixo de 30%, vão descansar),
- **Fome** (se acima de 70%, vão comer),
- **Explosões** (que acontecem ao clicar no mapa).

O sistema de pânico foi modelado para que agentes a uma distância inferior a **3 metros** do centro da explosão corram para longe da mesma.
### Implementação Técnica
- `SpecIA`: Gerencia a lógica individual de cada agente.
- `GameBrain`: Gerencia o cenário e a criação dos agentes.
- **NavMesh**: Utilizado para navegação automática.
- **OverlapSphere**: Utilizado para detecção de explosões e efeitos no raio de impacto.
---

## Resultados e Discussão
Todos os agentes sempre se comportaram dentro das normas, apenas fomos apanhados de surpresa com alguns dos fatores aleatórios, mas nada fora do previsto.

O que mais observamos foi confusão, sobretudo caos, mesmo sem as explosões. Cada agente ao pensar diferente do outro, mostra-nos como cada indivíduo é único e ainda assim consegue mostrar parecenças.
Infelizmente não alcançamos todos os objetivos, nomeadamente o das mesas e de aviso consoante a explosão, mas estamos confiantes que quanto ao que fizemos, está bastante dentro do esperado.
---
### Conclusão
Dado ao facto de cada agente, unicamente, se comportar e mostrar esses comportamentes de forma diferente, descobrimos como também podemos avaliar seres humanos tendo em base robôs.

Foi bastante esclarecedor, durante o desenvolvimento do projeto, como teriamos de dividir e demonstrar o nosso conhecimento quanto à implementação de Inteligência Artificial e aprendendo a mesma para melhor demonstração.

---
## Referências

[Unity NavMesh Tutorial - YouTube](https://www.youtube.com/watch?v=CHV1ymlw-P8)
[Unity Documentation - Physics.OverlapSphere](https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Physics.OverlapSphere.html)
[Unity Documentation - ScriptReference](https://docs.unity3d.com/ScriptReference/)
ChatGPT usado para dúvidas no SpecIA, usado parcialmente no FireSpreading

## Agradecimentos

Queremos agradecer a alguns dos nosso colegas que nos ajudaram com algumas duvidas durante o projecto:

Lee Dias
Ricardo Almeida