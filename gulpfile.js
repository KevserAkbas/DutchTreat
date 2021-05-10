var gulp = require("gulp");
var uglify = require("gulp-uglify");
var concat = require("gulp-concat");
//minify JavaScript
function minify() {
    return 
    .src(["wwwroot/js/**/*.js"])
        .pipe(uglify())//uglify tüm dosyayı sıkıştırdı
        .pipe(concat("dutchtreat.min.js"))//sıkıştırılmış dosyalrı birleştirdi
        .pipe(gulp.dest("wwwroot/dist/"));//ve bu hedefe tükürdü
}

//minify CSS
function styles() {
    return gulp.src(["wwwroot/css/**/*.css"])
        .pipe(uglify())//uglify tüm dosyayı sıkıştırdı
        .pipe(concat("dutchtreat.min.css"))//sıkıştırılmış dosyalrı birleştirdi
        .pipe(gulp.dest("wwwroot/dist/"));//ve bu hedefe tükürdü
}
exports.minify = minify;
exports.styles = styles;
exports.default = gulp.parallel(minify, styles); //bu dosyanın varsayılan dışa aktarımı sadece küçültmeyi çalıştırmak olacaktır