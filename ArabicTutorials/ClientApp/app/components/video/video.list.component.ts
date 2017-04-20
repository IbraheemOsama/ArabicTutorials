import { Component } from '@angular/core';
import { VideoQueryService } from '../../services/video.query.service';
import { Http } from '@angular/http';
import { BaseDetails } from '../../models';

@Component({
    selector: 'video-list',
    template: require('./video.list.component.html')
})
export class VideoListComponent {
    public videos: BaseDetails[];

    constructor(private videoQueryService: VideoQueryService) {
        videoQueryService.getAll().subscribe(videos => this.videos = videos,
            error => console.error('Error: ' + error),
            () => console.log('Completed!'));
    }

    //public handleNewlyAddedBook(book: Book) {
    //    this.books.push(book);
    //}

    //public removeBook(book: Book) {
    //    this.bookService.delete(book).subscribe();
    //    let index: number = this.books.indexOf(book);
    //    if (index !== -1) {
    //        this.books.splice(index, 1);
    //    }
    //}
}
