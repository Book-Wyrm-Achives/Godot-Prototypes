shader_type canvas_item;

uniform vec2 AspectRatio = vec2(1.0);
uniform vec4 Color : source_color = vec4(vec3(0.0), 1.0);
uniform float Thickness = 0.05;
uniform vec2 RelativeStartPoint = vec2(0.0);
uniform vec2 RelativeEndPoint = vec2(1.0);


float dot(vec2 v1, vec2 v2) {
	return v1.x * v2.x + v1.y * v2.y;
}

vec2 proj(vec2 v, vec2 on) {
	return on * dot(v, on) / dot(on, on);
}

vec2 rej(vec2 v, vec2 from) {
	return v - proj(v, from);
}

void fragment() {
	vec2 baseUV = UV * AspectRatio;
	vec2 startPoint = RelativeStartPoint * AspectRatio;
	vec2 endPoint = RelativeEndPoint * AspectRatio;
	
	COLOR = vec4(0.0);
	
	if(length(rej(baseUV - startPoint, endPoint - startPoint)) <= Thickness / 2.0) {
		COLOR = Color;
	}
}
