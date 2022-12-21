import { Component, TemplateRef } from '@angular/core';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

import { Evento } from '../../../models/Evento';
import { EventoService } from '../../../services/evento.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss'],
})
export class EventoListaComponent {
  modalRef: any = BsModalRef;
  public eventos: Evento[] = [];
  public filteredEvents: Evento[] = [];

  private _filterList: string = '';
  public hideImage: boolean = true;

  public get filterList(): string {
    return this._filterList;
  }

  public set filterList(filter: string) {
    this._filterList = filter;

    this.filteredEvents = this.filterList
      ? this.handleFilterEvents(this.filterList)
      : this.eventos;
  }

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe({
      next: this.handleUpdateEventos.bind(this),
      error: this.handleError.bind(this),
      complete: () => this.spinner.hide(),
    });
  }

  public handleUpdateEventos(Evento: Evento[]): void {
    this.eventos = Evento;
    this.filteredEvents = Evento;
  }

  public handleError(error: Error): void {
    this.spinner.hide();
    this.toastr.error(
      'Ocorreu um erro no sistema, tente novamente mais tarde',
      'Ocorreu um erro!'
    );
  }

  public handleHideImage(): void {
    this.hideImage = !this.hideImage;
  }

  public handleFilterEvents(filter: string): Evento[] {
    filter = filter.toLocaleLowerCase();

    return this.eventos.filter(
      (evento: any) =>
        evento.tema.toLocaleLowerCase().indexOf(filter) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filter) !== -1 ||
        evento.dataEvento.indexOf(filter) !== -1
    );
  }

  public detalheEvento(id: number): void {
    this.router.navigate([`/eventos/detalhe/${id}`]);
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success(
      'O evento foi removido com sucesso',
      'Evento removido!'
    );
  }

  decline(): void {
    this.modalRef?.hide();
  }
}
