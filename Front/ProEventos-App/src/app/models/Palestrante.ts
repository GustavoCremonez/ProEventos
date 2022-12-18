import { RedeSocial } from './RedeSocial';
import { PalestranteEvento } from './PalestranteEvento';

export interface Palestrante {
  id: number;
  name: string;
  miniCurriculo: string;
  imagemURL: string;
  telefone: string;
  email: string;
  redesSociais: RedeSocial[];
  palestrantesEventos: PalestranteEvento[];
}
