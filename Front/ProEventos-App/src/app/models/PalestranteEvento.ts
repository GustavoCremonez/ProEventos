import { Palestrante } from './Palestrante';
import { Evento } from './Evento';

export interface PalestranteEvento {
  palestranteid: number;
  palestrante: Palestrante;
  eventoid: number;
  evento: Evento;
}
