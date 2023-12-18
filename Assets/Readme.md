# Green & Phygital Experience in Genova - Mercato 3D

![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white)
![WebGL](https://img.shields.io/badge/WebGL-990000?logo=webgl&logoColor=white&style=for-the-badge)
---



## Indice

- [Descrizione](#descrizione)
- [Requisiti](#requisiti)
- [Struttura](#struttura)
- [Scene](#scene)
- [Configurazione](#configurazione)
  - [Variabili d'ambiente](#variabili-dambiente)
- [Uso](#uso)
  - [Build](#build)
  - [Release](#release)
  - [Note](#note)
- [Licenze](#licenze)
- [Riferimenti](#riferimenti)


### Descrizione
Il seguente repository contiene i sorgenti del progetto **Mercato 3D**, verranno
elencati i requisiti di sviluppo e le sue parti, la sua struttura e le eventuali note utili.


### Requisiti

- [Unity 2021.3.14f1](https://unity.com/releases/editor/whats-new/2021.3.14)
- IDE avente compatibilità con Unity, ad esempio [Rider](https://www.jetbrains.com/rider/)

### Struttura

```yaml
.
├── AssetBundles                    # AssetBundles scena phygital
├── phygital                        # Cartella principale Mercato 3D
│    ├── Fbx                        # Modelli, materiali, texture
│    ├── Font                       # Font utilizzati
│    ├── import                     # Ulteriori Modelli, materiali, texture
│    ├── Json                       # Metodi e modelli C# per recupero, gestione ed invio dati
│    ├── MainMenu                   # Risorse relative alla schermata di caricamento
│    ├── Scenes                     # Scene utilizzate da Unity
│    ├── Scripts                    # Codice C# relativo al Mercato 3D
│    ├── Search in Hierarchy        # Asset di ricerca con id indicizzati, per ricerca con nome durante lo sviluppo
├──  └── UI                         # Interfaccia
└── Plugins                         # Plugins del progetto implementati
     └── WebGL                      # Metodi javascript usati per comunicazione con il portale visitgenoa.it
```

### Scene

```yaml
Main Menu                           # Caricamento Mercato 3D
phygital                            # Mercato 3D
```

### Configurazione

#### Variabili d'ambiente

Vedere `Assets/phygital/Scripts/Env.cs`


### Uso

Per lo sviluppo della scena phygital, caricarla direttamente dalla tab *hierarchy* senza passare dalla scena del caricamento

#### Build

Effettuare la build prima degli AssetBundles e poi della sola scena *phygital*

- **AssetBundles** `Assets > Build AssetBundles`
- **phygital** `File > Build Settings`
  1. Selezionare solo la scena *phygital* tra quelle disponibili
  2. Platform > WebGL
  3. Dal menu a tendina disponibile sul pulsante *Build* selezionare *Clean Build*
  4. usare *release* come nome in quanto attualmente stabilito e configurato nei puntamenti del portale visitgenoa.it
  5. comprimere in un archivio ZIP la cartella risultante della build


#### Release

1. Copiare l'archivio con la build della scena *phygital* in `<root-mercato-3d>/Sviluppo/Builds`
2. estrarre il contenuto ed impostare i permessi adeguati per la cartella risultante (nome uguale all'archivio) ed i file contenuti affinchè siano leggibili dal server
3. Copiare i file AssetBundles in `<root-mercato-3d>/Sviluppo/Resources/AssetBundles`

#### Note

Richiedere i dati sensibili necessari allo sviluppo, rimossi alla pubblicazione del codice sorgente

### Licenze

Esistono alcune limitazioni circa il riuso diffuso delle librerie di modelli 3d utilizzate nel mercato.

Gli arredi, i banchi e gli alimenti rappresentati in 3d sono realizzati a partire da modelli commerciali.

Le licenze di tali prodotti commerciali escludono il riutilizzo dei componenti medesimi su altri ambiti e quindi la riesportazione per altri progetti.
Caso diverso è quello dell’ambiente architettonico, (comprese le interpareti divisorie e le travature), delle botteghe storiche e dei negozi di categoria che sono state realizzate ad hoc

### Riferimenti

[Documentazione Unity 2021.3.14f1](https://docs.unity3d.com/2021.3/Documentation/Manual/index.html)
