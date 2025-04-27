# Projeto 1
## Intelig�ncia Artificial

**Ivan Emidio a22301234**  
**Paulo Silva a22206205**

---

## Contribui��es

### Ivan Emidio:
- Script `SpecIA`: Essencial da FSM (65% do script).
- Script `Explosion`: (90% do script).
- Script `ExplosionManager`: Tudo.
- Script `FireSpread`: (90% do script).
- Adicionei Prefabs `Fire`, `Capsule` (player).
- Criei bot�o para dar Spawn de mais agentes, e o respetivo m�todo `SpawnMoreAgents`.

### Paulo Silva:
- Script `SpecIA`: Essencial da FSM (35% do script)
- Script `Explosion`: (10% do script)..
- Script `FireSpread`: (10% do script).
- Cria��o do cen�rio.
- Cria��o de texto UI
- Script `GameBrainer`: Tudo
- Cria��o do NavMesh e do Agent
- Organiza��o e cria��o dos terrenos para NavMesh

---

## Introdu��o

O projeto desenvolvido consiste numa simula��o de v�rios agentes durante um evento de concertos utilizando Intelig�ncia Artificial no Unity.  
O objetivo principal foi criar agentes que, de forma aut�noma, reagissem a diferentes necessidades (fome e cansa�o) e a eventos inesperados (explos�es).

Para resolver o problema, foi implementado um sistema de intelig�ncia baseado numa **M�quina de Estados Finita (FSM)**, que permite aos agentes mudarem de estado conforme as suas necessidades.

T�nhamos como ideia principal tamb�m adicionar um sistema de propaga��o de p�nico onde agentes pr�ximos de outros em fuga reagissem da mesma maneira, mas n�o conseguimos implementar a tempo essa rea��o.  
No entanto, ficou implementada a rea��o de p�nico dos agentes que se encontram mais pr�ximos da explos�o, fugindo para o lado contr�rio ao centro da explos�o.

Os resultados alcan�ados confirmaram o sucesso da implementa��o da FSM nos agentes, comportando-se de forma realista e adaptativa de acordo com as suas necessidades, al�m da demonstra��o de comportamentos emergentes durante situa��es de p�nico.

---

## Metodologia

A simula��o foi implementada em **3D** utilizando a engine **Unity**.

O cen�rio principal consiste numa �rea de concertos dividida em:
- Tr�s palcos (concertos),
- Duas zonas verdes (zonas de descanso),
- Duas zonas de alimenta��o.

Adicionalmente, foram criadas zonas de entrada e sa�da para permitir a evacua��o dos agentes em caso de p�nico (embora n�o estejam operacionais).

O movimento dos agentes foi implementado de forma **din�mica**, utilizando a **NavMesh** do Unity, que calcula e percorre caminhos automaticamente.

Cada agente possui:
- Velocidade base,
- Velocidade em p�nico,
- Velocidade reduzida.

Foi utilizada uma **M�quina de Estados Finita (FSM)** para controlar os comportamentos dos agentes.  
Cada agente pode estar num dos seguintes estados:
- `Idle` (inicial),
- `WatchingConcert` (assistir a concertos),
- `GoingToGreenZone` (descansar),
- `GoingToFoodZone` (comer).

A mudan�a de estado depende dos seguintes fatores:
- **Energia** (se abaixo de 30%, v�o descansar),
- **Fome** (se acima de 70%, v�o comer),
- **Explos�es** (que acontecem ao clicar no mapa).

O sistema de p�nico foi modelado para que agentes a uma dist�ncia inferior a **3 metros** do centro da explos�o corram para longe da mesma.
### Implementa��o T�cnica
- `SpecIA`: Gerencia a l�gica individual de cada agente.
- `GameBrain`: Gerencia o cen�rio e a cria��o dos agentes.
- **NavMesh**: Utilizado para navega��o autom�tica.
- **OverlapSphere**: Utilizado para detec��o de explos�es e efeitos no raio de impacto.
---

## Resultados e Discuss�o
Todos os agentes sempre se comportaram dentro das normas, apenas fomos apanhados de surpresa com alguns dos fatores aleat�rios, mas nada fora do previsto.

O que mais observamos foi confus�o, sobretudo caos, mesmo sem as explos�es. Cada agente ao pensar diferente do outro, mostra-nos como cada indiv�duo � �nico e ainda assim consegue mostrar parecen�as.
Infelizmente n�o alcan�amos todos os objetivos, nomeadamente o das mesas e de aviso consoante a explos�o, mas estamos confiantes que quanto ao que fizemos, est� bastante dentro do esperado.
---
### Conclus�o
Dado ao facto de cada agente, unicamente, se comportar e mostrar esses comportamentes de forma diferente, descobrimos como tamb�m podemos avaliar seres humanos tendo em base rob�s.

Foi bastante esclarecedor, durante o desenvolvimento do projeto, como teriamos de dividir e demonstrar o nosso conhecimento quanto � implementa��o de Intelig�ncia Artificial e aprendendo a mesma para melhor demonstra��o.

---
## Refer�ncias

[Unity NavMesh Tutorial - YouTube](https://www.youtube.com/watch?v=CHV1ymlw-P8)
[Unity Documentation - Physics.OverlapSphere](https://docs.unity3d.com/6000.1/Documentation/ScriptReference/Physics.OverlapSphere.html)
[Unity Documentation - ScriptReference](https://docs.unity3d.com/ScriptReference/)
ChatGPT usado para d�vidas no SpecIA, usado parcialmente no FireSpreading

## Agradecimentos

Queremos agradecer a alguns dos nosso colegas que nos ajudaram com algumas duvidas durante o projecto:

Lee Dias
Ricardo Almeida