# 09 — View today’s surgery

Commit: `build getTodaysSurgery`. PS: `ps/12`.

## getTodaySurgery

```typescript
getTodaySurgery() {
  this._curewellService.getAllSurgeriesForToday().subscribe({
    next: (response) => {
      this.surgeryList = response;
      console.log("Today's Surgery Fetched Successfully");
    },
    error: (error) => {
      this.surgeryList = null;
      this.errorMsg = error;
      this.showMsgDiv = true;
    }
  });
}
```

`ngOnInit` only calls `this.getTodaySurgery()`.

## editSurgery (navigation, not the HTTP call)

```typescript
editSurgery(surgery: Surgery) {
  this.router.navigate([
    '/editSurgery',
    surgery.doctorId,
    surgery.endTime,
    surgery.startTime,
    surgery.surgeryCategory,
    surgery.surgeryDate,
    surgery.surgeryId
  ]);
}
```

Button: `(click)="editSurgery(item)"`.

The update component reads params by those names. See [04](04-router.md) and [12](12-update-surgery.md).

## HTML date

```html
{{item.surgeryDate | date:"dd/MM/yyyy" }}
```

`date` pipe formats the string from the API. It does not change what you pass into `navigate`.
