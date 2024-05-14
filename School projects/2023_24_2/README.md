# Automata raktárrendszer

https://szofttech-ab-2024.szofttech.gitlab-pages.hu/group-04/csapat2/

A szoftver egy automata szimulációs raktárrendszer program, ahol valós időben le lehet futtatni egy raktárban szállító robotok mozgását, továbbá lehetőségünk van kielemezni azaz vissza játszani egy futás eredményét valós időben, lassítva, gyorsítva, egy adott részre ugorva és ott akár előre hátra léptetve megtekinteni azt.

A raktár négyzethálóba van szervezve. Vannak benne robotok, célállomások, és raktári állványok. A robotok feladata, hogy folyamatosan a számukra kijelölt célállomásokhoz menjenek el.  A robotok diszkrét időpontonként lépnek egyszerre, mindegyik úgy, ahogy a központi ütemező utasítja.

A szimuláció indításához a központi vezérlőnek a szimuláció elején meg kell adni egy konfigurációs fájlban:
- a raktár elrendezését (méret, állványok) 
- a robotok számát
- a robotok kiindulási helyét 
- a célállomások helyét időrendi sorrendben 
- hány új célállomást lát előre az ütemező  
- a célállomások kiosztási stratégiáját (ez egyelőre „roundrobin”, vagyis folyamatosan az éppen 
- felszabaduló robotnak, több felszabaduló robot esetén abban a sorrendben, ahogy a  kiindulási helyek fájljában szerepelnek)

Továbbá, hogy  hány másodpercig tart egy szimulációs lépés, és hogy hány szimulációs lépést akarunk végrehajtani.

A szimuláció végén egy naplófájlban meg akarjuk kapni:
- robotmozgás szabályainak neve 
- minden lépés ütközésmentes volt-e 
- a robotok száma 
- a robotok kiindulási helye 
- összesen hány feladatot hajtottak végre 
- összesen hány műveletet hajtottak végre 
- hány lépésig tartott a szimuláció 
- minden robotra, hogy ténylegesen milyen műveleteket hajtott végre
- minden robotra, hogy az ütemező mit jelölt ki számára
- minden ütemező válaszra, hogy az ütemező mennyi idő után adta ki a következő műveletet 
- hibák listája
- feladat események listája
- feladatok listája

A naplófájlt a szimulációs programba betöltve újra le tudjuk futtatni a szimulációt, de itt már vizsgálódni is tudunk: tudjuk gyorsítva vagy lassítva lejátszani, meg tudjuk állítani, előre-hátra tudjuk léptetni, tetszőleges szimulációs lépésre rá tudunk állni. 

## Projekt megvalósíthatósági tanulmánya
[WIKI - Követelményrendszer](https://szofttech.inf.elte.hu/szofttech-ab-2024/group-04/csapat2/-/wikis/Program-k%C3%B6vetelm%C3%A9nyrendszer)

[WIKI - Mockup](https://szofttech.inf.elte.hu/szofttech-ab-2024/group-04/csapat2/-/wikis/Mockup)

[WIKI - Osztály és csomag diagrammok](https://szofttech.inf.elte.hu/szofttech-ab-2024/group-04/csapat2/-/wikis/Oszt%C3%A1ly-%C3%A9s-csomag-diagrammok)

[WIKI - Felhasználói történetek](https://szofttech.inf.elte.hu/szofttech-ab-2024/group-04/csapat2/-/wikis/User-stories)
