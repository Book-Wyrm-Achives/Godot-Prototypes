shader_type canvas_item;

uniform vec4 Color : source_color = vec4(vec3(0.0), 1.0);

void fragment() {
	vec2 baseUV = UV * 2.0;
	baseUV -= 1.0;
	
	COLOR = vec4(0.0);
	
	if(length(baseUV) < 1.0) {
		COLOR = Color;
	}
}
