============= Informationen ================

Autor: Andreas Korb
Beginn: 21.10.2013
Ende: ???

============================================
========== Vektorendefinition ==============

----------> x
|
|
|
|
V
y

Y zeigt nach unten, da es so leichter ist, Zeile für Zeile zu zeichnen.

Maximale Zeichen des Standard-Konsolenfensters:

Spalte: 35
Zeile: 25
Insgesamt: 875

============================================
================ Neuzeichnung ==============

Snake: Positionsänderung alle 100 ms => häufige Zeichnung
Candy: sobald eingesammelt => seltene Zeichnung
Wall: immer gleiche Stelle => einmalige Zeichnung

============================================
================== Tastatur ================

Es wird immer nur die zuletzt gedrückte Taste behandelt.

Zunächst wird geguckt, um welche Pfeiltaste es sich handelt. Anschließend wird geprüft, ob der Richtungswechsel zulässig ist.
Nicht zulässig ist er, sobald der Richtungswechsel eine 180° Drehung darstellen würde ( z.B. links zu rechts ).

============================================
==================== Q&A ===================

Q: Wieso abonnierst du nicht einfach das KeyDown Event um Pfeiltasten zu behandeln?
A: Konsolenanwendungen stellen kein KeyDown Event zur Verfügung.