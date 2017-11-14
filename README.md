# UNO-Game
Trabalho final SI 3º período

#Plataforma: C# WPF

Jogo de cartas simples composto por 1 jogador real, e 3 BOTs. 

# Regras: 
> **Objetivo**: ser o primeiro jogador a ficar sem cartas na mão, utilizando todos os meios possíveis para impedir que os outros jogadores façam o mesmo.

> **Como jogar**: Cada jogador recebe 7 cartas. O restante do baralho é deixado na mesa com a face virada para baixo e então vira-se uma carta do monte. Esta carta que fica em cima da mesa serve como base para que o jogo comece.
O jogador a esquerda do que distribuiu as cartas inicia o jogo, que deve seguir em sentido horário. Os jogadores devem jogar, na sua vez, uma carta de mesmo número, cor, OU símbolo da carta que está na mesa. Exemplo: se a carta inicial for um 2 vermelho o primeiro jogador deve jogar sobre ela um 2 (não importando a cor) ou uma carta vermelha (não importando o número). O jogador sucessivo faz o mesmo, dessa vez valendo como base a carta colocada pelo jogador anterior.
Ao jogar a penúltima carta, o jogador deve anunciar em voz alta falando “UNO". Se não fizer isso, os demais jogadores podem obrigá-lo a comprar mais duas cartas. A rodada termina quando um dos jogadores zerar as suas cartas na mão.

> **Cartas especiais**: Além das cartas numéricas, o baralho de UNO possui mais 5 cartas especiais que produzem diferentes efeitos 
durante o jogo.

> **+2**: o jogador seguinte apanha duas cartas e passa o seu turno ao jogador seguinte.

> **Inversão**: o sentido de jogo inverte-se. Se o sentido do jogo está no sentido horário, quando jogada uma carta "Inverter", joga-se em sentido anti-horário.

> **Bloqueio**: o jogador seguinte perde a vez.

> **Curinga**: pode ser jogada durante qualquer momento do jogo independentemente da carta que se encontra no topo de descarte. O participante que jogar essa carta escolhe a próxima cor do jogo (verde, azul, vermelho ou amarelo).

> **Curinga +4**: o jogador seguinte apanha quatro cartas do baralho e perde o turno, o jogador que a descartou diz escolhe a próxima cor do jogo (verde, azul, vermelho ou amarelo). Esta carta só deverá ser jogada quando o jogador não não possui nenhuma outra carta que possa usar. No entanto, se o jogador prejudicado desconfiar que o primeiro jogador está “blefando”, pode pedir para conferir a mão deste, se estiver certo, o jogador que jogou terá que apanhar as 4 cartas como punição. Caso a jogada tenha sido legal, o jogador que desconfiou deve apanhar seis cartas.

# Cartas:

No total são 108 cartas, distribuídas da seguinte forma:

19 cartas azuis - de 0 a 9;

►19 cartas verdes - de 0 a 9; 

►19 cartas vermelhas - de 0 a 9; 

►19 cartas amarelas - de 0 a 9; 

► 8 cartas +2 - duas de cada cor; 

► 8 cartas “Bloquear” - duas de cada cor; 

► 8 cartas “Inverter” - duas de cada cor; 

► 4 cartas “Coringa (Muda de cor)”; 

► 4 cartas +4 
